using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponHandle 
{
    void Setup(WeaponBehavior wp);
    void FireHandle();

    void ReadyHandle();
    
}
