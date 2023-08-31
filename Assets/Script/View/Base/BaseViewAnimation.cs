using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
public class BaseViewAnimation : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private void Awake()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
            canvasGroup.alpha = 0;
    }
    // Start is called before the first frame update
    public virtual void ShowViewAnimation(Action callBack)
    {
        canvasGroup.DOFade(1, 0.5f).OnComplete(() =>
        {
            callBack();

        }).SetUpdate(true);
    }
    public virtual void HideViewAnimation(Action callBack)
    {
        canvasGroup.DOFade(0, 0.5f).OnComplete(() =>
        {
            callBack();

        }).SetUpdate(true);
    }
}
