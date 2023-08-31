using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class ConfigWeaponLevelRecord
{
    //id	name	description	prefab	cost	damage	range	rof	clipsize
    [SerializeField]
    private int id;
    public int ID { get { return id; } }

    [SerializeField]
    private int level;
    public int Level { get { return level; } }
    [SerializeField]
    private int cost;
    public int Cost { get { return cost; } }
    
    [SerializeField]
    private int damage;
    public int Damage { get { return damage; } }
    [SerializeField]
    private int range;
    public int Range { get { return range; } }
    [SerializeField]
    private float rof;
    public float ROF { get { return rof; } }
    [SerializeField]
    private int clipsize;
    public int Clipsize { get { return clipsize; } }

}
public class ConfigWeaponLevel : BYDataTable<ConfigWeaponLevelRecord>
{
    public override ConfigCompare<ConfigWeaponLevelRecord> DefindConfigCompare()
    {
        return new ConfigCompare<ConfigWeaponLevelRecord>("id","level");
    }

    public ConfigWeaponLevelRecord GetMaxLevel(int id_wp)
    {
        return records.Where(x => x.ID == id_wp).OrderByDescending(x => x.Level).FirstOrDefault();
    }
}
