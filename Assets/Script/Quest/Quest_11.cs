using UnityEngine;
using System.Collections;

public class Quest_11 : QuestItemControl {
     public override void Setup(ConfigQuestRecord configQuest)
     {
         base.Setup(configQuest);
     }
     public override void LogQuest(QuestLogData logData)
     {
         base.LogQuest(logData);
      
        if (configQuest.Quest_type == logData.questType)
        {
            Debug.Log("aaaaaaa Q11");
            bool isWin = (bool)logData.data;
            if (!isWin)
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
