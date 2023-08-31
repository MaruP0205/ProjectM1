using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public class ConfigMissionRecord
{
    [SerializeField]
    private int id;
    public int ID { get { return id; } }
    [SerializeField]
    private int stage;
    public int Stage { get { return stage; } }
    [SerializeField]
    private string name;
    public string Name { get { return name; } }
   

    [SerializeField]
    private string description;
    public string Description { get { return description; } }
    [SerializeField]
    private string sceneName;
    public string SceneName { get { return sceneName; } }
    [SerializeField]
    private string waves;
    public List<string> Waves 
    { 
        get 
        {
            string[] s = waves.Split(';');
            List<string> ls = new List<string>();
            ls.AddRange(s);
            return ls;
        } 
    }
    [SerializeField]
    private int reward;
    public int Reward { get { return reward; } }
}
public class ConfigMission : BYDataTable<ConfigMissionRecord>
{
    public override ConfigCompare<ConfigMissionRecord> DefindConfigCompare()
    {
        return new ConfigCompare<ConfigMissionRecord>("id");
    }

    public List<ConfigMissionRecord> GetConfigByStage(int stage_ID)
    {
        return records.Where(x=>x.Stage == stage_ID).ToList();
    }

    public int GetTotalStarByMission(int id)
    {
        return records.Where(x => x.ID <= id-1).Count()*3;
    }
}
