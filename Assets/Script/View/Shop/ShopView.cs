using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopView : View
{
    public ShopViewItem item_prefab;
    public Transform parent_item;
    private List<ShopViewItem> ls_items = new List<ShopViewItem>();
    public override void Setup(ViewParam param)
    {
        
        List<ConfigShopRecord> configs = ConfigManager.instance.configShop.GetAllRecords();
        if (ls_items.Count <= 0)
        {
            for(int i =0; i< configs.Count; i++ )
            {
                ShopViewItem item = Instantiate(item_prefab);
                item.transform.SetParent(parent_item, false);
                item.transform.SetSiblingIndex(i);
                ls_items.Add(item);
            }
        }
        int count = 0;
        foreach (ConfigShopRecord cf in configs)
        {
            ls_items[count].Setup(cf);
            count++;
        } //Ví dụ thứ 1


    }
}
