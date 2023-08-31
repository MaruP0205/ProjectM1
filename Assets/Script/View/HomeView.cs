using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeView : View
{
    public override void Setup(ViewParam param)
    {
        base.Setup(param);
        if (param != null)
        {
            HomeViewParam viewParam = (HomeViewParam)param;
        }
    }

    public override void ShowView()
    {
        base.ShowView();
    }

    public override void HideView()
    {
        base.HideView();
    }

    public void OnShowWeaponView()
    {
        WeaponViewParam param = new WeaponViewParam();
        param.weapon_id = 2;
        ViewManager.instance.SwitchView(ViewIndex.WeaponView,param);
    }
    
    public void OnShop()
    {
        ViewManager.instance.SwitchView(ViewIndex.ShopView);
    }
    
    public void OnQuest()
    {
        ViewManager.instance.SwitchView(ViewIndex.QuestView);
    }
    
    public void OnPlay()
    {
        ViewManager.instance.SwitchView(ViewIndex.MissionView);
    }
}
