using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieControl : EnemyControl
{
    public ZombieDataBinding dataBinding;
    public NavMeshAgent agent;
    // Start is called before the first frame update
    public Zombie_Attack_State attack_State;
    public Zombie_Avade_State avade_State;
    public Zombie_FindAid_State findAid_State;
    public Zombie_Wander_State wander_State;
    public Zombie_Idle_State idle_State;
    public Zombie_Dead_State dead_State;
    [SerializeField]
    private Transform root_pos;
    private BulletData data;

    public override void Setup(EnemyInitData enemyInitData)
    {
        base.Setup(enemyInitData);
        root_pos = enemyInitData.root_pos;
        attack_State.parent = this;
        avade_State.parent = this;
        findAid_State.parent = this;
        wander_State.parent = this;
        idle_State.parent = this;
        dead_State.parent = this;
        agent = GetComponent<NavMeshAgent>();
        GotoState(idle_State);
    }

    public Vector3 GetRandomPos()
    {
        float x = UnityEngine.Random.Range(-6f, 6f);
        float z = UnityEngine.Random.Range(-6f, 6f);
        Vector3 pos = root_pos.position + new Vector3(x, 0, z);
        return pos;
    }
    public void Bite()
    {
        Attack();
        //  Debug.LogError(name_);
    }
    public override void OnDamage(BulletData bulletData)
    {
        if (hp <= 0)
            return;
        enemyHub.ShowEffect(hp, maxhp);
        hp -= bulletData.damage;
        if (hp <= 0)
        {
            if(current_state != dead_State)
            {
                bodyType_dead = bulletData.bodyType;
                GotoState(dead_State);
                this.data = bulletData;
                Invoke("PlayPhysic", 0.1f);
            }
        }
        base.OnDamage(bulletData);

    }
    private void PlayPhysic()
    {
        data.rb.AddForceAtPosition(data.dir.normalized * data.force, data.point, ForceMode.Impulse);
    }
    public override void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hp <= 0)
            return;
        if (other.gameObject.CompareTag("Player"))
        {
            CheckTarget(other.transform);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (hp <= 0)
            return;
        if (other.gameObject.CompareTag("Player"))
        {
            CheckTarget(other.transform);

        }
    }
    private void CheckTarget(Transform target)
    {
        if (current_state != attack_State)
        {
            Vector3 pos = target.position;
            pos.y = trans.position.y;
            Vector3 dir = pos - trans.position;
            dir.Normalize();
            float dot = Vector3.Dot(trans.forward, dir);
            if (dot >= angleDetect) 
            {
                GotoState(attack_State, target);

            }
        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (hp <= 0)
            return;
        if (other.gameObject.CompareTag("Player"))
        {
            GotoState(wander_State);
            Debug.Log("OnTriggerExit");

        }
    }

}
