using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Zombie_Idle_State : FSM_State
{
    [NonSerialized]
    public ZombieControl parent;
    private Coroutine coroutine_;
    public override void OnEnter()
    {
        parent.dataBinding.SpeedMove = 0;
        if(coroutine_!=null)
        {
            parent.StopCoroutine(coroutine_);
        }
        coroutine_ = parent.StartCoroutine(DelaySwitchState(3));
    }
    IEnumerator DelaySwitchState(float delay)
    {
        yield return new WaitForSeconds(3);
        parent.GotoState(parent.wander_State);
    }
    public override void OnUpdate()
    {

       

    }
    public override void Exit()
    {
        if (coroutine_ != null)
        {
            parent.StopCoroutine(coroutine_);
        }
    }

}
