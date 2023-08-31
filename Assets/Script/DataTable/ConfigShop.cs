using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ConfigShopRecord
{
    //id	name	description	prefab	cost	damage	range
    [SerializeField]
    private int id;
    [SerializeField]
    private string name;
    public string Name { get { return name; } }
    [SerializeField]
    private string description;
    public string Description { get { return description; } }
    [SerializeField]
    private int cost;
    public int Cost { get { return cost; } }
    [SerializeField]
    private int value;
    public int Value { get { return value; } }

}
public class ConfigShop : BYDataTable<ConfigShopRecord>
{
    public override ConfigCompare<ConfigShopRecord> DefindConfigCompare()
    {
        return new ConfigCompare<ConfigShopRecord>("id");
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
