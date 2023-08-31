using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogMissionInfo : BaseDialog
{
    public Text mission_name;
    public Text mission_des;
    public Text reward_lb;
    private DialogMissionInfoParam dl_param;

    public override void Setup(DialogParam param)
    {
        dl_param = (DialogMissionInfoParam)param;
        mission_name.text = dl_param.config.Name;
        mission_des.text = dl_param.config.Description;
        reward_lb.text = dl_param.config.Reward.ToString();
        base.Setup(param);
    }

    public void OnClose()
    {
        DialogManager.instance.HideDialog(this.dialogIndex);
    }

    public void OnPlay()
    {
        ViewManager.instance.SwitchView(ViewIndex.EmptyView);
        DialogManager.instance.HideDialog(this.dialogIndex);
        LoadSceneManager.instance.LoadSceneByName(dl_param.config.SceneName, () =>
        {
            GameManager.instance.CreateMission(dl_param.config);
        });
    }
}
