using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterControl : MonoBehaviour
{
    public CharacterController characterController_;
    public CharacterDataBinding dataBinding_;
    public float speedMove = 2;
    [SerializeField]
    private WeaponControl weaponControl;

    private Transform trans;
    public Transform enemy_trans;
    [SerializeField]
    private float velocity_y = 0;
    private float a = -9.8f;
    private Vector3 move_dir_jump;
    public Transform andchor_foot;
    [SerializeField]
    private bool isGrounded = false;
    public LayerMask maskGround;
    public LayerMask maskEnemy;
    public float radius = 5;
    public Transform anchoDetect;
    private WeaponBehavior cur_weapon;
    private float rangeDetech;
    public int maxHP = 100;
    private int hp;
    public event Action<int, int> OnHPChange;
    private EnemyControl cur_e_;
    public EnemyControl cur_enemy
    {
        set
        {
            cur_e_ = value;
        }
        get
        {
            if (cur_e_ == null)
            {
                if (enemy_trans != null)
                {
                    cur_e_ = enemy_trans.GetComponentInParent<EnemyControl>();
                }
            }
            return cur_e_;

        }
    }

   /* public bool isAim => enemy_trans != null ; 
      public bool isAim 
        {
            get { return enemy_trans != null; }
        }
   */
    private void Awake()
    {
        hp = maxHP;
        trans = transform;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.position = SceneConfig.instance.Pos_Player.position;
        StartCoroutine("DetectEnemy");
        weaponControl.OnChangeGun += OnChangeGun;
    }

    private void OnChangeGun(WeaponBehavior weapon)
    {
        cur_weapon = weapon;
        rangeDetech = weapon.range;
    }

    public void OnDamage(EnemyDamageData damageData) //Nhận damage
    {
        hp -= damageData.damage;
        OnHPChange?.Invoke(hp, maxHP);
        if(hp <= 0)
        {
            DialogManager.instance.ShowDialog(DialogIndex.DialogFail);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        float speed = enemy_trans != null ? speedMove : speedMove * 2f; //Viết tắt của if (nếu isAim thỏa thì làm 1 còn không thì làm 2 
        Vector3 moveDir = InputManager.move_dir;
        float t = Time.deltaTime;
        if (moveDir.magnitude > 0)
        {
            if (enemy_trans!=null)
            {
                Vector3 pos = enemy_trans.position;
                pos.y = trans.position.y;
                Vector3 dir = pos - trans.position;
                Quaternion q = Quaternion.LookRotation(dir.normalized, Vector3.up);
                trans.localRotation = q;
               // trans.forward = Vector3.forward;
                dataBinding_.SpeedMove = 1;
                Vector3 move_Anim = trans.InverseTransformDirection(moveDir);
                dataBinding_.MoveDir = move_Anim;
            }
            else
            {
                trans.forward = moveDir;
                dataBinding_.SpeedMove = 2;
                dataBinding_.MoveDir = Vector3.forward;
                cur_enemy = null;
            }
        }
        else
        {
            dataBinding_.MoveDir = Vector3.zero;
            dataBinding_.SpeedMove = 0;
            if (enemy_trans != null)
            {
                Vector3 pos = enemy_trans.position;
                pos.y = trans.position.y;
                Vector3 dir = pos - trans.position;
                Quaternion q = Quaternion.LookRotation(dir.normalized, Vector3.up);
                trans.localRotation = q;
                
            }
            else
            {
                cur_enemy = null;

            }
        }

        RaycastHit hit;
        if(Physics.SphereCast(andchor_foot.position,0.3f, Vector3.down,out hit, 0.55f,maskGround)&&velocity_y < 0)
        {
            isGrounded = true;
        }else
        {
            isGrounded = false;
            if (Physics.SphereCast(andchor_foot.position, 0.1f, Vector3.down, out hit, 1.3f, maskGround))
            {
                dataBinding_.Ground = true;
            }
            else
            {
                dataBinding_.Ground = false;
            }
        }
        
        if (InputManager.isJump && isGrounded)
        {
            velocity_y = 5f;
            move_dir_jump = moveDir;
            dataBinding_.Jump = true;
        }

        if (!isGrounded)
        {
            if (velocity_y > 0)
            {
                move_dir_jump.y = 1.8f;
            }

            else
            {
                move_dir_jump.y = -1.6f;
            }
           
            characterController_.Move(move_dir_jump * speedMove * t);
            dataBinding_.MoveDir = Vector3.zero;
            dataBinding_.SpeedMove = 0;
        }
        else
        {
            
            characterController_.Move(moveDir * speedMove * t);
        }
        velocity_y = velocity_y + a * t;

        if (InputManager.isFire)
        {
            DetectEnemy();
        }
    }
    IEnumerator DetectEnemy() 
    {
        WaitForSeconds wait = new WaitForSeconds(0.05f);
        while (true)
        {
            yield return wait;
            if (enemy_trans == null && InputManager.isFire)
            {
                Collider[] cols = Physics.OverlapSphere(anchoDetect.position, rangeDetech, maskEnemy);
                List<EnemyDetectData> lsE = new List<EnemyDetectData>();
                foreach (Collider c in cols)
                {
                    EnemyDetectData enemyDetectData = new EnemyDetectData();
                    enemyDetectData.trans_p = anchoDetect;
                    enemyDetectData.trans_e = c.transform;
                    enemyDetectData.enemyControl = c.GetComponentInParent<EnemyControl>();
                    if (!Physics.Linecast(anchoDetect.position, c.transform.position, maskGround))
                    {
                        lsE.Add(enemyDetectData);
                    }

                }
                lsE.Sort(new EnemyDetectCompare());
                if (lsE.Count > 0)
                {
                    enemy_trans = lsE[0].trans_e;
                    cur_enemy = enemy_trans.GetComponentInParent<EnemyControl>();
                }
                else
                    enemy_trans = null;
            }

            if (!InputManager.isFire)
                enemy_trans = null;

            if (enemy_trans != null)
            {
                if(Vector3.Distance(trans.position, enemy_trans.position)>rangeDetech)
                {
                    enemy_trans = null;
                }
            }
        }
       
       
    }
    private void OnDisable()
    {
        weaponControl.OnChangeGun -= OnChangeGun;
    }
}

public class EnemyDetectData
{
    public Transform trans_p;
    public Transform trans_e;
    public EnemyControl enemyControl;

}

public class EnemyDetectCompare : IComparer<EnemyDetectData>
{
    public int Compare(EnemyDetectData x, EnemyDetectData y)
    {
        float d_x = Vector3.Distance(x.trans_e.position, x.trans_p.position);
        float d_y = Vector3.Distance(y.trans_e.position, y.trans_p.position);
        if (d_x > d_y) 
            return 1;
        else if(d_x < d_y) 
            return -1;
        else
        {
            Vector3 dir_x = x.trans_e.position; 
            dir_x.y = x.trans_p.position.y;
            dir_x = dir_x - x.trans_p.position;
            float dot_x = Vector3.Dot(x.trans_p.position, dir_x.normalized);

            Vector3 dir_y = y.trans_e.position;
            dir_y.y = y.trans_p.position.y;
            dir_y = dir_y - y.trans_p.position;
            float dot_y = Vector3.Dot(y.trans_p.position, dir_y.normalized);
            if (dot_x < dot_y)
                return 1;
            else if (d_x > d_y)
                return -1;
            else
                return 0;
        }
    }
}
