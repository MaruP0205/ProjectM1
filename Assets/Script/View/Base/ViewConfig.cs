using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ViewIndex
{
    EmptyView = 0,
    HomeView = 1,
    WeaponView = 2,
    MissionView = 3,
    ShopView = 4,
    QuestView = 5,

}
public class ViewParam
{

}
public class HomeViewParam : ViewParam
{
    public string mess;
}
public class WeaponViewParam : ViewParam
{
    public int weapon_id;
}
public class ViewConfig
{
    public static ViewIndex[] viewIndices =  {
        ViewIndex.EmptyView,
        ViewIndex.HomeView,
        ViewIndex.WeaponView,
        ViewIndex.ShopView,
        ViewIndex.QuestView,
        ViewIndex.MissionView
    };
}
