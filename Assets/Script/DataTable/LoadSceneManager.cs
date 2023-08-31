using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneManager : BYSingletonMono<LoadSceneManager>
{
    private Action callback;
    public float timeWait = 3;
    [Range(0, 1)]
    public float sampleWait = 0.5f;
    private float progress;
    public CanvasGroup canvasGroup;
    public Slider progress_Sl;
    public Text progress_Lb;
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup.gameObject.SetActive(false);
    }

    // Update is called once per frame

    public void LoadSceneByName(string sceneName, Action callback)
    {
        StopCoroutine("LoadSceneProgress");
        this.callback = callback;
        StartCoroutine("LoadSceneProgress", sceneName);
    }
    IEnumerator LoadSceneProgress(string sceneName)
    {
        canvasGroup.gameObject.SetActive(true);
        canvasGroup.DOFade(1, 0.5f);
        yield return new WaitUntil(() => canvasGroup.alpha >= 0.9f);
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        float timeCount = 0;
        bool isDone = false;
        progress = 0;
        while (!isDone)
        {
            if (timeCount < timeWait)
            {
                timeCount += 0.1f;
                progress = (timeCount / timeWait) * sampleWait;
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                progress = sampleWait + async.progress * (1 - sampleWait);
            }
            isDone = async.isDone && timeCount >= timeWait;
            progress_Sl.value = progress;
            progress_Lb.text = ((int)(progress * 100)).ToString() + "%";
        }
        yield return new WaitForSeconds(0.5f);
        canvasGroup.alpha = 0;
        canvasGroup.gameObject.SetActive(false);
        callback?.Invoke();
        callback = null;
    }
    public void LoadSceneByIndex(int index, Action callback)
    {
        StopCoroutine("LoadSceneProgress");
        this.callback = callback;
        StartCoroutine("LoadSceneProgress", index);
    }
    IEnumerator LoadSceneProgress(int index)
    {
        canvasGroup.gameObject.SetActive(true);
        canvasGroup.DOFade(1, 0.5f);
        yield return new WaitUntil(() => canvasGroup.alpha >= 0.9f);
        AsyncOperation async = SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
        float timeCount = 0;
        bool isDone = false;
        progress = 0;
        while (!isDone)
        {
            if (timeCount < timeWait)
            {
                timeCount += 0.1f;
                progress = (timeCount / timeWait) * sampleWait;
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                progress = sampleWait + async.progress * (1 - sampleWait);
            }
            isDone = async.isDone && timeCount >= timeWait;
            progress_Sl.value = progress;
            progress_Lb.text = ((int)(progress * 100)).ToString() + "%";
        }
        yield return new WaitForSeconds(0.5f);
        canvasGroup.alpha = 0;
        canvasGroup.gameObject.SetActive(false);
        callback?.Invoke();
        callback = null;
    }
}
