using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogWin : BaseDialog
{
    //public Text reward_lb;
    public override void ShowDialog()
    {
        Time.timeScale = 0;
        base.ShowDialog();
    }

    public override void HideDialog()
    {
        Time.timeScale = 1;
        base.HideDialog();
    }

    public override void Setup(DialogParam param)
    {
        base.Setup(param);
        dialogWinParam dl_param = (dialogWinParam)param;
        int reward;
        DataAPIController.instance.UpdateMission(dl_param, out reward);
        //reward_lb.text = reward.ToString();
        var data = (object)true;
        QuestManager.instance.LogQuest(new QuestLogData { questType = QuestType.PLAY_MISSION, data = data });
    }
    public void OnQuitMission()
    {
        DialogManager.instance.HideDialog(dialogIndex);
        LoadSceneManager.instance.LoadSceneByIndex(1, () =>
        {
            ViewManager.instance.SwitchView(ViewIndex.HomeView);
        });
    }
}
