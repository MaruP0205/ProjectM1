using Newtonsoft.Json.Linq;
using RootMotion.Dynamics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDataBinding : MonoBehaviour
{
    public Animator animator;
    private Vector3 pre_pos;
    private float speed_;
    public float factor = 2;
    private float current_Speed;
    private Transform trans;

    public float SpeedMove
    {
        set
        {
            speed_ = value;
        }
       
    }
    public bool Attack
    {
        set
        {
            if(value)
            {
                int index = UnityEngine.Random.Range(1, 3);
                animator.SetInteger(Key_IndexAttack, index);
                animator.SetTrigger(Key_Attack);

            }
        }

    }
    public bool Dead
    {
        set
        {
            if (value)
            {
                puppet.mode = PuppetMaster.Mode.Active;
                puppet.Kill(stateSettings);
               // puppet.pinWeight = 0f;
               // puppet.muscleWeight = 0.1f;
            }
        }
    }
    private int Key_Speed;
    private int Key_Attack;
    private int Key_IndexAttack;
    public PuppetMaster.StateSettings stateSettings = PuppetMaster.StateSettings.Default;
    public PuppetMaster puppet;
    // Start is called before the first frame update
    void Start()
    {
        Key_Speed = Animator.StringToHash("Speed");
        Key_Attack = Animator.StringToHash("Attack");
        Key_IndexAttack = Animator.StringToHash("IndexAttack");
        trans = transform;
        pre_pos = trans.position;
    }

    // Update is called once per frame
    void Update()
    {
        float dis = Vector3.Distance(trans.position, pre_pos);
        pre_pos = trans.position;
        float sp = dis / Time.deltaTime;
        current_Speed = Mathf.Lerp(current_Speed, sp*factor, Time.deltaTime*5);
        animator.SetFloat(Key_Speed, current_Speed);
    }
}
