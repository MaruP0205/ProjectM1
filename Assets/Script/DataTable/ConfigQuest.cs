using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestType
{
    KILL_ENEMY = 1,
    UPGRADE_GUN = 2,
    PLAY_MISSION = 3,
    BUY_GUN = 4,
    LOG_IN = 5,
}
[Serializable]
public class ConfigQuestRecord
{
    [SerializeField]
    private int id;
    public int ID
    {
        get
        {
            return id;
        }
    }
    [SerializeField]
    private QuestType questtype;
    public QuestType Quest_type
    {
        get
        {
            return questtype;
        }
    }
    [SerializeField]
    private string name;
    public string Name { get { return name; } }
    [SerializeField]
    private string description;
    public string Description { get { return description; } }
    [SerializeField]
    private int number;
    public int Number { get { return number; } }
    [SerializeField]
    private int reward;
    public int Reward { get { return reward; } }


}
public class ConfigQuest : BYDataTable<ConfigQuestRecord>
{
    public override ConfigCompare<ConfigQuestRecord> DefindConfigCompare()
    {
        return new ConfigCompare<ConfigQuestRecord>("id");
    }

}
