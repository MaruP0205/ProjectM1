using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;
using System.Reflection;

[Serializable]
public class Zombie_Wander_State : FSM_State
{
    [NonSerialized]
    public ZombieControl parent;
    private float timer;
    private Vector3 target;
    public float speed_move = 1;
    private int count;
    public NavMeshPath path;
    private int index = -1;
    private Vector3 cur_pos;
    private Coroutine coroutine_;
    public override void OnEnter()
    {
        if (path==null)
             path = new NavMeshPath();
        
        target = parent.GetRandomPos();
        parent.dataBinding.SpeedMove = 1;
        parent.agent.isStopped = false;
        parent.agent.updatePosition = false;
        parent.agent.updateRotation = false;
        count = 0;
        GetPointRandomMove();
    }
    public override void OnUpdate()
    {
        timer += Time.deltaTime;

        if (Vector3.Distance(parent.trans.position,target)<=0.5f|| timer > 5)
        {
         
            count++;
            if(count>=3)
            {
                parent.GotoState(parent.idle_State);
            }
            else
            {
                GetPointRandomMove();
            }
        }
       
    }

    private void GetPointRandomMove()
    {
        if (coroutine_ != null)
        {
            parent.StopCoroutine(coroutine_);
        }

        target = parent.GetRandomPos();
        timer = 0;

        parent.agent.Warp(parent.trans.position);
        parent.agent.CalculatePath(target, path);
        if (path.status != NavMeshPathStatus.PathInvalid)
        {
            if (path.corners.Length > 1)
            {
                
                coroutine_ = parent.StartCoroutine(LoopWalk());
            }
        }
    }
    public override void Exit()
    {
        parent.dataBinding.SpeedMove = 0;
        parent.agent.isStopped = true;
        if (coroutine_ != null)
        {
            parent.StopCoroutine(coroutine_);
        }
    }


    IEnumerator LoopWalk()
    {
        WaitForSeconds wait = new WaitForSeconds(0.02f);
        index = 1;
        while (index < path.corners.Length)
        {
            cur_pos = path.corners[index];
            float dis = Vector3.Distance(parent.trans.position, cur_pos);
            if (dis <= 0.3f)
            {
                index++;

            }
            else
            {
                Vector3 dir = cur_pos - parent.trans.position;
                if (dir.magnitude > 0)
                {
                    dir.Normalize();
                    Quaternion q = Quaternion.LookRotation(dir, Vector3.up);
                    parent.trans.rotation = Quaternion.Slerp(parent.trans.rotation, q, Time.deltaTime * 360);
                }
                parent.trans.position = Vector3.MoveTowards(parent.trans.position, cur_pos, Time.deltaTime * speed_move);
            }
            yield return wait;
        }
        parent.agent.Warp(parent.trans.position);
    }
}
