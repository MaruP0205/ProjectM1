using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
public class BaseDialogAnimation : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private void Awake()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0;
        }
    }
    // Start is called before the first frame update
    public virtual void ShowDialogAnimation(Action callback)
    {
        
        canvasGroup.DOFade(1, 0.3f).OnComplete(() =>
        {
            callback();
        }).SetUpdate(true);
    }

    public virtual void HideDialogAnimation(Action callback)
    {
        canvasGroup.DOFade(0, 0.2f).OnComplete(() =>
        {
            callback();
        }).SetUpdate(true);
    }
}
