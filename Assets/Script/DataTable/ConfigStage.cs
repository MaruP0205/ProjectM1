using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ConfigStageRecord
{
    [SerializeField]
    private int id;
    public int ID { get { return id; } }
    [SerializeField]
    private string name;
    public string Name { get { return name; } }
    [SerializeField]
    private string description;
    public string Description { get { return description; } }
    [SerializeField]
    private int star;
    public int Star { get { return star; } }

}
public class ConfigStage : BYDataTable<ConfigStageRecord>
{
    public override ConfigCompare<ConfigStageRecord> DefindConfigCompare()
    {
        return new ConfigCompare<ConfigStageRecord>("id");
    }
}
