using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponView : View
{
    public WeaponViewItem item_prefab;
    public Transform parent_item;
    private List<WeaponViewItem> ls_items = new List<WeaponViewItem>();        
    public override void Setup(ViewParam param)
    {
        List<ConfigWeaponRecord> configs = ConfigManager.instance.configWeapon.GetAllRecords();
        if (ls_items.Count <= 0)
        {
            for (int i = 0; i < configs.Count; i++)
            {
                WeaponViewItem item = Instantiate(item_prefab);
                item.transform.SetParent(parent_item, false);
                item.transform.SetSiblingIndex(i);
                ls_items.Add(item);
            }
        }
        int count = 0;
        foreach (ConfigWeaponRecord cf in configs)
        {
            ls_items[count].Setup(cf);
            count++;
        } //Ví dụ thứ 1
    }

    public override void ShowView()
    {
        base.ShowView();
    }

    public override void HideView()
    {
        base.HideView();
        foreach(WeaponViewItem item in ls_items)
        {
            item.HideItem();
        }
    }

    public void OnShowHomeView()
    {
        HomeViewParam param = new HomeViewParam();
        param.mess = "Mess from WeaponView";
        ViewManager.instance.SwitchView(ViewIndex.HomeView,param);
    }
}
