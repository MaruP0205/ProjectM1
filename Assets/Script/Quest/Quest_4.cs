using UnityEngine;
using System.Collections;

public class Quest_4 : QuestItemControl {
     public override void Setup(ConfigQuestRecord configQuest)
     {
         base.Setup(configQuest);
     }
     public override void LogQuest(QuestLogData logData)
     {
        //E_005
        if (configQuest.Quest_type == logData.questType)
        {
            EnemyQuestLogData data = (EnemyQuestLogData)logData;
            if (data.cf_enemy.ID == "E_005")
                DataAPIController.instance.UpdateQuestData(configQuest.ID);
        }
        base.LogQuest(logData);
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
