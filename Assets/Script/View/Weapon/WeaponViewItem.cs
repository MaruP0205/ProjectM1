using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponViewItem : MonoBehaviour
{
    public Text name_lb;
    public Image icon;
    private ConfigWeaponRecord cf;
    public Text level_lb;
    public Text slot_lb;
   // public Text costLB;
    private ItemData dataGun;
    public GameObject lockObject;
    private ConfigWeaponLevelRecord cf_level;
    private ConfigWeaponLevelRecord cf_next_level;
    private ConfigWeaponLevelRecord cf_level_max;
    // Start is called before the first frame update
    public void Setup(ConfigWeaponRecord cf)
    {
        this.cf = cf;
        dataGun = DataAPIController.instance.GetWeaponData(cf.ID);
        SetInfo(dataGun);
        UpdateSlot(null);

        name_lb.text = cf.Name;
        icon.overrideSprite = SpriteLiblaryControl.instance.GetSpriteByName(cf.Icon);
        icon.SetNativeSize();
        DataTrigger.RegisterValueChange(DataPath.GUNS + "/" + cf.ID.ToKey(), SetInfo);

        DataTrigger.RegisterValueChange(DataPath.SLOT, UpdateSlot);
    }

    public void HideItem()
    {
        DataTrigger.UnRegisterValueChange(DataPath.GUNS + "/" + cf.ID.ToKey(), SetInfo);

        DataTrigger.UnRegisterValueChange(DataPath.SLOT, UpdateSlot);
    }

    private void SetInfo(object data)
    {
        dataGun = (ItemData)data;
        cf_level_max = ConfigManager.instance.configWeaponLevel.GetMaxLevel(cf.ID);
        if (dataGun != null)
        {
            cf_level = ConfigManager.instance.configWeaponLevel.GetRecordByKeySearch(cf.ID, dataGun.level); 
            level_lb.text = "Lv " + dataGun.level.ToString();
            if (cf_level.Level == cf_level_max.Level)
            {
                level_lb.text = "Max Lv";

            }
            else
            {
                level_lb.text = "Lv " + dataGun.level.ToString();
                
            }
        }
        else
        {
            level_lb.text = "";
            //costLB.text = cf.Cost.ToString();

        }
        lockObject.SetActive(dataGun == null);

    }
    private void UpdateSlot(object val)
    {
        if (dataGun != null)
        {
            
            int slot = DataAPIController.instance.GetSlot(cf.ID);
            if (slot >= 0)
            {
                slot += 1;
                slot_lb.text = "SLOT " + slot.ToString();
            }
            else
            {
                slot_lb.text = "";
            }
        }
        else
        {
            slot_lb.text = "";
        }
    }

    public void OnShowInfo()
    {
        DialogManager.instance.ShowDialog(DialogIndex.DialogWeaponInfo, new DialogWeaponInfoParam { config = cf, itemData = dataGun });
    }
}
