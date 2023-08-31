using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLogData
{
    public QuestType questType;
    public object data;
}

public class EnemyQuestLogData : QuestLogData
{
    public ConfigEnemyRecord cf_enemy;
    public BodyType bodyType;
}
public class QuestManager : BYSingletonMono<QuestManager>
{
    public event Action<QuestLogData> OnLogQuestEvent;
    public void InitQuest(Action callBack)
    {
        CreateQuest();
        callBack?.Invoke();
    }
    private void CreateQuest()
    {

        foreach (ConfigQuestRecord cf in ConfigManager.instance.configQuest.GetAllRecords())
        {
            GameObject quest_object = Instantiate(Resources.Load("Quest/Quest_" + cf.ID.ToString(), typeof(GameObject))) as GameObject;
            quest_object.transform.SetParent(transform);
            QuestItemControl questItemControl = quest_object.GetComponent<QuestItemControl>();
            questItemControl.Setup(cf);
            this.OnLogQuestEvent += questItemControl.LogQuest;
        }
    }
    public void LogQuest(QuestLogData questLogData)
    {
        OnLogQuestEvent?.Invoke(questLogData);
    }
}
