using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyInitData
{
    public ConfigEnemyRecord config;
    public Transform root_pos;
}

public class EnemyDamageData
{
    public int damage;
}
public class EnemyControl : FSM_System
{
   
    private string name_;
    [SerializeField]
    protected int hp;
    public int maxhp;
    public int damage;
    [NonSerialized]
    public Transform trans;
    [HideInInspector]
    public float timeAttack;
    public float attackRate = 3f;
    public float attackRange = 1.5f;
    [Range(-1f, 1f)]
    public float angleDetect = 0.5f;
    public EnemyHub enemyHub;
    public Transform anchorHub;
    public List<EnemyOnDamage> enemyOnDamages;
    protected ConfigEnemyRecord cf_enemy;
    public BodyType bodyType_dead;

    private void Awake()
    {
        trans = transform;
        enemyOnDamages = trans.GetComponentsInChildren<EnemyOnDamage>().ToList();
    }
    //  private
    //protected 
    public virtual void Setup(EnemyInitData enemyInitData)
    {
        maxhp = hp;
        enemyHub = PoolManager.instance.Spawn(NamePool.ENEMY_HUB).GetComponent<EnemyHub>();
        enemyHub.Setup(IngameUI.instance.parentEnemyHub, anchorHub);
        damage = enemyInitData.config.Damage;
        hp = enemyInitData.config.HP;
        cf_enemy = enemyInitData.config;
    }
    public virtual void OnDamage(BulletData bulletData)
    {
        // Debug.LogError("Enemy OnDamage: "+damage);
    }
    public void Attack()
    {
        Debug.LogError("attack");
       // OnDamage(2);
    }
    public override void Update()
    {
        base.Update();
        timeAttack += Time.deltaTime;
    }
    public void OnDead()
    {
        PoolManager.instance.DeSpawn(NamePool.ENEMY_HUB,enemyHub.transform);
        MissionManager.instance.OnEnemyDead(this);

        QuestManager.instance.LogQuest(new EnemyQuestLogData { questType = QuestType.KILL_ENEMY, cf_enemy = cf_enemy, bodyType = bodyType_dead });
        Invoke("DeplayDestroy", 2);
    }
    private void DeplayDestroy()
    {
        Destroy(gameObject);
    }

    public Transform GetTargetShoot(BodyType bodyType)
    {
        if(bodyType == BodyType.HEAD)
        {
            return enemyOnDamages.Where(x => x.bodyType == BodyType.HEAD).FirstOrDefault().transform;
        }
        else
        {
            return enemyOnDamages.OrderBy(x => Guid.NewGuid()).FirstOrDefault().transform;

        }
    }
}
public class Bullet
{
    public int damage;
    public Bullet()
    {

    }
    public Bullet(int damage)
    {
        this.damage = damage;
        Debug.LogError(" bullet : " + damage);
    }
}