using UnityEngine;
using System.Collections;

public class Quest_7 : QuestItemControl {
     public override void Setup(ConfigQuestRecord configQuest)
     {
         base.Setup(configQuest);
     }
     public override void LogQuest(QuestLogData logData)
     {
         base.LogQuest(logData);
        if (configQuest.Quest_type == logData.questType)
        {
            ConfigWeaponRecord cf = (ConfigWeaponRecord)logData.data;
            if(cf.WeaponType_ == WeaponType.ASSAULT)
            {
                Debug.Log("Quest 7");
                DataAPIController.instance.UpdateQuestData(configQuest.ID);

            }
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
