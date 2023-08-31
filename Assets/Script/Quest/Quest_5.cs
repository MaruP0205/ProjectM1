using UnityEngine;
using System.Collections;

public class Quest_5 : QuestItemControl {
     public override void Setup(ConfigQuestRecord configQuest)
     {
         base.Setup(configQuest);
     }
     public override void LogQuest(QuestLogData logData)
     {
         base.LogQuest(logData);
        if (configQuest.Quest_type == logData.questType)
        {
            EnemyQuestLogData data = (EnemyQuestLogData)logData;
            if (data.bodyType == BodyType.HEAD)
                DataAPIController.instance.UpdateQuestData(configQuest.ID);
        }
    }
    public override void CheckQuest()
     {
         base.CheckQuest();
     }
     void Start()
     {
     }
     void Update()
     {
     }
}
