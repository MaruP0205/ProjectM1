using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDialog : MonoBehaviour
{
    public DialogIndex dialogIndex;
    private BaseDialogAnimation baseDialogAnimation;
    private void Awake()
    {
        baseDialogAnimation = gameObject.GetComponentInChildren<BaseDialogAnimation>();
    }
    public virtual void Setup(DialogParam param)
    {

    }
    public void ShowDialogAnimation(Action callback)
    {
        baseDialogAnimation.ShowDialogAnimation(() =>
        {
            callback?.Invoke();
            ShowDialog();
            
        });
    }

    public void HideDialogAnimation(Action callback)
    {
        baseDialogAnimation.HideDialogAnimation(() =>
        {
            callback?.Invoke();
            HideDialog();
        });
    }
    public virtual void ShowDialog()
    {

    }
     
    public virtual void HideDialog() 
    {
        
    }
}
