using UnityEngine;
using System.Collections;

public class Quest_9 : QuestItemControl {
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
            ItemData gunData = DataAPIController.instance.GetWeaponData(cf.ID);
            ConfigWeaponLevelRecord cf_level = ConfigManager.instance.configWeaponLevel.GetRecordByKeySearch(cf.ID, gunData.level);
            ConfigWeaponLevelRecord cf_level_max = ConfigManager.instance.configWeaponLevel.GetMaxLevel(cf.ID);

            if (cf_level.Level == cf_level_max.Level)
            {
                Debug.Log("Quest 9");
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
