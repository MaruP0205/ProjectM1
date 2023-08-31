using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogFail : BaseDialog
{
    public override void Setup(DialogParam param)
    {
        base.Setup(param);
        var data = (object)false;
        QuestManager.instance.LogQuest(new QuestLogData { questType = QuestType.PLAY_MISSION, data = data });

    }
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

    public void OnQuitMission()
    {
        DialogManager.instance.HideDialog(dialogIndex);
        LoadSceneManager.instance.LoadSceneByIndex(1, () =>
        {
            ViewManager.instance.SwitchView(ViewIndex.HomeView);
        });
    }
}
