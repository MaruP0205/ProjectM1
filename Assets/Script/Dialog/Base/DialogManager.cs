using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : BYSingletonMono<DialogManager> 
{
    public Transform anchorDialog;
    private Dictionary<DialogIndex, BaseDialog> dic_dialog = new Dictionary<DialogIndex, BaseDialog>();
    private List<BaseDialog> ls_dialogShow = new List<BaseDialog>();
    //public BaseDialog currentView;
    public event Action<DialogIndex> OnPreViewSwitch; 
    public event Action<DialogIndex> OnViewSwitch;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f);
        foreach (DialogIndex dialogIndex in DialogConfig.dialogIndices)
        {
            string dialog_name = dialogIndex.ToString();
            GameObject dialog_go = Instantiate(Resources.Load("Dialog/" + dialog_name, typeof(GameObject))) as GameObject;
            dialog_go.transform.SetParent(anchorDialog, false);
            dialog_go.SetActive(false);
            dic_dialog.Add(dialogIndex, dialog_go.GetComponent<BaseDialog>());
        }
    }

    public void ShowDialog(DialogIndex index_, DialogParam param = null, Action callback = null)
    {

        BaseDialog dialog = dic_dialog[index_];
        if (!ls_dialogShow.Contains(dialog))
        {
            ls_dialogShow.Add(dialog);
        }
        dialog.gameObject.SetActive(true);
        dialog.Setup(param);
        dialog.ShowDialogAnimation(callback);

    }

    public void HideDialog(DialogIndex index_, Action callback = null)
    {
        BaseDialog dialog = dic_dialog[index_];
        if (ls_dialogShow.Contains(dialog))
        {
            ls_dialogShow.Remove(dialog);
        }

        dialog.HideDialogAnimation(() =>
        {
            callback?.Invoke();
            dialog.gameObject.SetActive(false);
        });
    }

    public void HideAllDialog()
    {
        foreach (BaseDialog dl in ls_dialogShow)
        {
            dl.HideDialogAnimation(null);
            dl.gameObject.SetActive(false);
        }
        ls_dialogShow.Clear();
    }
}
