using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class ConfigWeaponRecord
{
    //id	name	description	prefab	cost	damage	range	rof	clipsize
    [SerializeField]
    private int id;
    public int ID { get { return id; } }
    [SerializeField]
    private WeaponType weaponType;
    public WeaponType WeaponType_ { get { return weaponType; } }
    [SerializeField]
    private string name;
    public string Name { get { return name; } }
    [SerializeField]
    private string description;
    public string Description { get { return description; } }
    [SerializeField]
    private string icon;
    public string Icon { get { return icon; } }
    [SerializeField]
    private string prefab;
    public string Prefab { get { return prefab; } }
    [SerializeField]
    private int cost;
    public int Cost { get { return cost; } }

}
public class ConfigWeapon : BYDataTable<ConfigWeaponRecord>
{
    public override ConfigCompare<ConfigWeaponRecord> DefindConfigCompare()
    {
        return new ConfigCompare<ConfigWeaponRecord>("id");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
