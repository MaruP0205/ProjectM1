using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    public ViewIndex viewIndex;
    private BaseViewAnimation baseViewAnimation;
    private void Awake()
    {
        baseViewAnimation = gameObject.GetComponentInChildren<BaseViewAnimation>();
    }
    public virtual void Setup(ViewParam param)
    {

    }
    public void ShowViewAnimation(Action callBack)
    {
        baseViewAnimation.ShowViewAnimation(() =>
        {
            callBack();
            ShowView();
            
        });
    }
    public void HideViewAnimation(Action callBack)
    {
        baseViewAnimation.HideViewAnimation(() =>
        {
            callBack();
            HideView();
        });
    }
    public virtual void ShowView()
    {

    }
    public virtual void HideView()
    {

    }
}
