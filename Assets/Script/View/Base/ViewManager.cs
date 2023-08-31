using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager : BYSingletonMono<ViewManager> 
{
    public Transform anchorView;
    private Dictionary<ViewIndex, View> dic_Views = new Dictionary<ViewIndex, View>();
    public View currentView;
    public event Action<ViewIndex> OnPreViewSiwtch;
    public event Action<ViewIndex> OnViewSiwtch;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f);
        foreach (ViewIndex viewIndex in ViewConfig.viewIndices)
        {
            string view_name = viewIndex.ToString();
            GameObject view_go = Instantiate(Resources.Load("View/" + view_name, typeof(GameObject))) as GameObject;
            view_go.transform.SetParent(anchorView, false);
            view_go.SetActive(false);
            dic_Views.Add(viewIndex, view_go.GetComponent<View>());
        }
        yield return new WaitForSeconds(0.2f);
        SwitchView(ViewIndex.EmptyView);
    }

    // Update is called once per frame
    public void SwitchView(ViewIndex newView, ViewParam param = null, Action callback = null)
    {
        if (currentView != null)
        {
            currentView.HideViewAnimation(() =>
            {
                currentView.gameObject.SetActive(false);
                ShowNextView(newView, param, callback);
            });
        }
        else
        {
            ShowNextView(newView, param, callback);
        }
    }
    private void ShowNextView(ViewIndex newView, ViewParam param = null, Action callback = null)
    {
        currentView = dic_Views[newView];
        currentView.gameObject.SetActive(true);
        currentView.Setup(param);
        OnPreViewSiwtch?.Invoke(currentView.viewIndex);

        currentView.ShowViewAnimation(() =>
        {
            callback?.Invoke();
            OnViewSiwtch?.Invoke(currentView.viewIndex);
        });
    }

}
