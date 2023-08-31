using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeViewAnimation : BaseViewAnimation
{
    public Animator animator;
    private Action callback;
    public override void HideViewAnimation(Action callback)
    {
        this.callback = callback;
        animator.Play("Hide");
    }

    public override void ShowViewAnimation(Action callback)
    {
        this.callback = callback;
        animator.Play("Show");
    }

    public void ShowAnim()
    {
        callback?.Invoke();
    }

    public void HideAnim()
    {
        callback?.Invoke();
    } 
}

