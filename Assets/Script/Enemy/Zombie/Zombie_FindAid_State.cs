using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Zombie_FindAid_State : FSM_State
{
    [NonSerialized]
    public ZombieControl parent;
}
