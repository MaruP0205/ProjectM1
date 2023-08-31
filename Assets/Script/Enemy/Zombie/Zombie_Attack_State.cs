using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Reflection;
using UnityEngine.AI;

[Serializable]
public class Zombie_Attack_State : FSM_State
{
    [NonSerialized]
    public ZombieControl parent;
    private Transform target;
    public float speedChase = 2;
    private bool isAttack_process = false;
    private float timeDelay = 0;
    
    public override void OnEnter(object data)
    {
        target = (Transform)data;
        timeDelay = 0;
        isAttack_process = false;
        parent.agent.isStopped = false;
        parent.agent.Warp(parent.trans.position);
        parent.agent.updatePosition = true;
        parent.agent.updateRotation = false;
        parent.agent.speed = speedChase;

    }
    public override void OnUpdate()
    {
        timeDelay += Time.deltaTime;
        if (!isAttack_process)
        {
                  
            if (Vector3.Distance(target.position, parent.trans.position) <= parent.attackRange)
            {
                parent.dataBinding.SpeedMove = 0;
                if (parent.timeAttack >= parent.attackRate)
                {
                    parent.agent.isStopped = true;
                    parent.dataBinding.SpeedMove = 0;
                    parent.timeAttack = 0;
                    parent.dataBinding.Attack = true;
                    timeDelay = 0;
                }
            }
            else
            {
                if(timeDelay > 2)
                {
                    parent.agent.isStopped = false;
                    parent.dataBinding.SpeedMove =2 * parent.agent.velocity.magnitude / speedChase;
                    parent.agent.SetDestination(target.position);
                    UpdateRotation();
                }
                
            }
        }
       
    }

    private void UpdateRotation()
    {
        Vector3 Pos_Steering = parent.agent.steeringTarget;
        Vector3 dir = Pos_Steering - parent.trans.position;
        if (dir.magnitude > 0)
        {
            dir.Normalize();
            Quaternion q = Quaternion.LookRotation(dir, Vector3.up);
            parent.trans.rotation = Quaternion.Slerp(parent.trans.rotation, q, Time.deltaTime * 360);
        }


    }

    public override void OnAnimMiddle()
    {
        isAttack_process = true;
       
        //check distance & direction
        Vector3 pos = target.position;
        pos.y = parent.trans.position.y;
        Vector3 dir = pos - parent.trans.position;
        dir.Normalize();
        float dot = Vector3.Dot(parent.trans.forward, dir);
        if (dot >= parent.angleDetect && Vector3.Distance(parent.trans.position, pos) < parent.attackRange)
        {
            target.GetComponent<CharacterControl>().OnDamage(new EnemyDamageData { damage= parent.damage});
        }
        
    }

    public override void OnAnimEnter()
    {
        isAttack_process = false;
    } 

    public override void Exit()
    {
        parent.dataBinding.SpeedMove = 0;
        parent.agent.isStopped = true;
        
    }
}
