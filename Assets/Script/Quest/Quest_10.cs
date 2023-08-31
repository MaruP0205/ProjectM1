using UnityEngine;
using System.Collections;

public class Quest_10 : QuestItemControl {
     public override void Setup(ConfigQuestRecord configQuest)
     {
         base.Setup(configQuest);
     }
     public override void LogQuest(QuestLogData logData)
     {
        if (configQuest.Quest_type == logData.questType)
        {
            Debug.Log("aaaaaaa Q10");
            bool isWin = (bool)logData.data;
            if (isWin)
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
