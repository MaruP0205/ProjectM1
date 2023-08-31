using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum WeaponType
{
    HAND_GUN = 1,
    SHOTGUN = 2,
    SNIPER = 3,
    ASSAULT = 4,
    MACHINE = 5,
    
}

public enum NamePool
{
    BULLET = 1,
    BL_SHOOTGUN = 2,
    BL_HANDGUN = 3,
    IP_METAL = 4,
    IP_FLESH = 5,
    ENEMY_HUB = 6,
    BL_ASSAULT = 7,
    BL_SNIPER = 8,
    BL_MACHINE = 9
}

public enum BodyType
{
    HEAD = 1,
    NORMAL = 2,
    LOW = 3,
    UP = 4,
}
public class GameConfig : MonoBehaviour
{
    public const string Tag_Flesh = "Flesh";
    public const string Tag_Metal = "Metal";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
