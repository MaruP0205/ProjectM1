using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    
public class QuestItemControl : MonoBehaviour
{
    public QuestData questData;
    public ConfigQuestRecord configQuest;
    public virtual void Setup(ConfigQuestRecord configQuest)
    {
        this.configQuest = configQuest;
        questData = DataAPIController.instance.GetQuestData(configQuest.ID);
    }

    public virtual void LogQuest(QuestLogData logData)
    {

    }

    public virtual void CheckQuest()
    { 
    
    }
}
