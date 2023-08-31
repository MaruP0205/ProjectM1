using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogPause : BaseDialog
{
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

    public void OnQuit()
    {
        DialogManager.instance.HideDialog(DialogIndex.DialogPause);
        LoadSceneManager.instance.LoadSceneByIndex(1, () =>
        {
            ViewManager.instance.SwitchView(ViewIndex.HomeView);
        });
    }

    public void OnResume()
    {
        DialogManager.instance.HideDialog(DialogIndex.DialogPause);
    }
}
