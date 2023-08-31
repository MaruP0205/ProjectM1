using EnhancedUI.EnhancedScroller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionView_StageCellView : EnhancedScrollerCellView
{
    public Text name_lb;
    public GameObject lock_Object;
    public Text start_need_lb;
    public List<MissionViewItem> items;
    public void SetData(MissionView_StageData data)
    {
        lock_Object.SetActive(data.sum_star < data.cf_stageRecord.Star&&data.cf_cur_Mission.Stage <= data.cf_stageRecord.ID);
        start_need_lb.text = "Need " + (data.cf_stageRecord.Star - data.sum_star).ToString();
        name_lb.text = data.cf_stageRecord.Name;
        List<ConfigMissionRecord> cf_Mission_Stage = ConfigManager.instance.configMission.GetConfigByStage(data.cf_stageRecord.ID);
        for(int i = 0; i < items.Count; i++)
        {
            if(i<cf_Mission_Stage.Count)
            {
                items[i].gameObject.SetActive(true);
                items[i].Setup(cf_Mission_Stage[i], data.cf_cur_Mission);
            }
            else
            {
                items[i].gameObject.SetActive(false);
            }
        }
    }
}
