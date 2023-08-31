using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData 
{
    public DateTime timeLastLogin;
    [SerializeField]
    public PlayerInfo info;
    [SerializeField]
    public MapInfo mapInfo;
    [SerializeField]
    public PlayerInventory inventory;
    [SerializeField]
    public Dictionary<string, QuestData> dic_quest= new Dictionary<string, QuestData>();

}

[Serializable]
public class PlayerInfo
{
    public string name;
    public int level;
    public int exp;
    [SerializeField]
    public List<int> guns_equip = new List<int>();
}

[Serializable]
public class MapInfo
{
    public int currentMission;
    [SerializeField]
    public Dictionary<string, MissionData> dic_Mission = new Dictionary<string, MissionData>();
}

[Serializable]
public class PlayerInventory
{
    [SerializeField]
    public Dictionary<string, ItemData> dic_guns = new Dictionary<string, ItemData>();
    public int gold;
}

[Serializable]
public class ItemData
{
    public int id;
    public int level;
}

[Serializable]
public class MissionData
{
    public int id;
    public int star;
}

[Serializable]
public class QuestData
{
    public int id = 0;
    public int number = 0;
    public bool is_claimed = false;
}