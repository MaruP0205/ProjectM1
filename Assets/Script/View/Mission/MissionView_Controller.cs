using EnhancedUI.EnhancedScroller;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionView_Controller : MonoBehaviour, IEnhancedScrollerDelegate
{
    private List<MissionView_StageData> stageDatas = new List<MissionView_StageData>();
    public float cell_size;
    public MissionView_StageCellView pf_MissionView_StageCellView;
    public EnhancedScroller myScroller;
    public Text start_lb;
    public MissionViewSnap viewSnap;
    private int index;

    public int Setup()
    {
        int total_star = DataAPIController.instance.GetTotalStarMission();
        ConfigMissionRecord cf_cur_mission = DataAPIController.instance.GetCurrentMission();
        if(stageDatas.Count == 0)
        {
            foreach (ConfigStageRecord cf in ConfigManager.instance.configStage.GetAllRecords())
            {
                MissionView_StageData stageData = new MissionView_StageData();
                stageData.cf_stageRecord = cf;
                stageData.cf_cur_Mission = cf_cur_mission;
                stageData.sum_star = total_star;
                stageDatas.Add(stageData);
            }
        }
        myScroller.Delegate = this;
        myScroller.ReloadData();
        index = cf_cur_mission.Stage - 1;
        viewSnap.MaxDataElements = stageDatas.Count;
        myScroller.JumpToDataIndex(index, 0, 0.125f, true, EnhancedScroller.TweenType.immediate, 0.1f);

        return stageDatas.Count;
    }
    #region IEnhancedScrollerDelegate Method
    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        MissionView_StageCellView cellView = scroller.GetCellView(pf_MissionView_StageCellView) as MissionView_StageCellView;
        cellView.SetData(stageDatas[dataIndex]);
        return cellView;
    }

    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return cell_size;
    }

    public int GetNumberOfCells(EnhancedScroller scroller)
    {
       return stageDatas.Count;
    }
    #endregion

    public void OnNext()
    {
        index++;
        if(index >= stageDatas.Count)
            index = stageDatas.Count - 1;
        myScroller.JumpToDataIndex(index, 0, 0.125f, true, EnhancedScroller.TweenType.easeInQuart, 0.5f);

    }

    public void OnPrevious()
    {
        index--;
        if(index < 0)
            index = 0;
        myScroller.JumpToDataIndex(index, 0, 0.125f, true, EnhancedScroller.TweenType.easeInQuart, 0.5f);

    }
}
