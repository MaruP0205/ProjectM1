using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Zombie_Dead_State : FSM_State
{
    [NonSerialized]
    public ZombieControl parent;
    public AudioSource deadsound;
    public override void OnEnter()
    {
        base.OnEnter();
        parent.dataBinding.Dead = true;
        deadsound.Play();
        parent.OnDead();
    }
}
