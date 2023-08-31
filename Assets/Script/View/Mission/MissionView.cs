using EnhancedUI.EnhancedScroller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionView : View
{
    public MissionView_Controller view_Control;
    public Text star_lb;

    public override void Setup(ViewParam param)
    {
        view_Control.Setup();
        /*for(int i = 1; i < 10; i++)
        {
            ConfigMissionRecord cf = ConfigManager.instance.configMission.GetRecordByKeySearch(i);
            DataAPIController.instance.UpdateMission(cf, UnityEngine.Random.Range(1, 4));
        }*/

        ConfigMissionRecord cf = DataAPIController.instance.GetCurrentMission();
        int total_star = DataAPIController.instance.GetTotalStarMission();
        int total_star_All = ConfigManager.instance.configMission.GetTotalStarByMission(cf.ID);
        star_lb.text = total_star.ToString() + "/" + total_star_All.ToString();
    }

    public void OnBack()
    {
        ViewManager.instance.SwitchView(ViewIndex.HomeView);
    }
}
