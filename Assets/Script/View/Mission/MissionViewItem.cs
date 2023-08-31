using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionViewItem : MonoBehaviour
{
    public Text mission_id;
    public GameObject lockObject;
    public GameObject star_parent;
    public GameObject[] stars;
    private ConfigMissionRecord cf;
    private MissionData missionData;
    public void Setup(ConfigMissionRecord cf, ConfigMissionRecord cf_cur_mission)
    {
        this.cf = cf;
        missionData = DataAPIController.instance.GetMissionData(cf);
        mission_id.text = cf.ID.ToString();
        if(cf.ID < cf_cur_mission.ID)
        {
            star_parent.SetActive(true);
            lockObject.SetActive(false);
            for (int i = 0; i < 3; i++)
            {
                stars[i].SetActive(i+1 <= missionData.star);
            }
        }
        else if (cf.ID == cf_cur_mission.ID)
        {
            star_parent.SetActive(true);
            lockObject.SetActive(false);
            for (int i = 0; i < 3; i++)
            {
                stars[i].SetActive(false);
            }
        }
        else
        {
            lockObject.SetActive(true);
            star_parent.SetActive(false);
            
        }
    }

    public void OnClick()
    {
        DialogManager.instance.ShowDialog(DialogIndex.DialogMissionInfo, new DialogMissionInfoParam { config = cf });
    }
}
