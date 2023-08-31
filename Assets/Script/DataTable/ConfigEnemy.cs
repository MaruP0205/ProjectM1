using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ConfigEnemyRecord
{
    [SerializeField]
    private string id;
    public string ID { get { return id; } }
    [SerializeField]
    private string name;
    public string Name { get { return name; } }
    [SerializeField]
    private string prefab;
    public string Prefab { get { return prefab; } }
    [SerializeField]
    private int damage;
    public int Damage { get { return damage; } }
    [SerializeField]
    private int hp;
    public int HP { get { return hp; } }
}
public class ConfigEnemy : BYDataTable<ConfigEnemyRecord>
{
    public override ConfigCompare<ConfigEnemyRecord> DefindConfigCompare()
    {
        return new ConfigCompare<ConfigEnemyRecord>("id");
    }
}
