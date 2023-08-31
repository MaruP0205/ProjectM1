using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogWeaponInfo : BaseDialog
{
    public Text gunName;
    public Image iconGun;
    public Text level_lb;
    public Text slot_lb;
    private DialogWeaponInfoParam dl_param;
    public DialogWeaponInfoElement damage_e;
    public DialogWeaponInfoElement rage_e;
    public DialogWeaponInfoElement rof_e;
    public DialogWeaponInfoElement clip_e;
    private ItemData dataGun;
    private ConfigWeaponRecord cf;
    public GameObject buy_group;
    public Button btn_buy;
    public Text cost_buy;
    public GameObject upgrade_group;
    public Button btn_upgrade;
    public Text cost_upgrade;
    public GameObject equip;
    private int gold;
    private ConfigWeaponLevelRecord cf_level;
    private ConfigWeaponLevelRecord cf_next_level;
    private ConfigWeaponLevelRecord cf_level_max;
    public override void Setup(DialogParam param)
    {
        base.Setup(param);
        gold = DataAPIController.instance.GetGold();
        dl_param = (DialogWeaponInfoParam)param;
        dataGun = dl_param.itemData;
        cf = dl_param.config;
        gunName.text = cf.Name;
        iconGun.overrideSprite = SpriteLiblaryControl.instance.GetSpriteByName(cf.Icon);
        SetInfo(dataGun);
        UpdateSlot(dataGun);
        DataTrigger.RegisterValueChange(DataPath.GUNS + "/" + cf.ID.ToKey(), SetInfo);
        DataTrigger.RegisterValueChange(DataPath.SLOT, UpdateSlot);
        DataTrigger.RegisterValueChange(DataPath.GOLD, UpdateGold);
    }

    private void UpdateGold(object val)
    {
        gold = (int)val;

    }
    private void SetInfo(object data)
    {
        dataGun = (ItemData)data;
        cf_level_max = ConfigManager.instance.configWeaponLevel.GetMaxLevel(cf.ID);
       
        buy_group.SetActive(dataGun == null);
        upgrade_group.SetActive(dataGun != null);
        equip.SetActive(dataGun != null);
        if (dataGun != null)
        {
            cf_level = ConfigManager.instance.configWeaponLevel.GetRecordByKeySearch(cf.ID, dataGun.level);
            upgrade_group.SetActive(cf_level.Level < cf_level_max.Level);
            if(cf_level.Level == cf_level_max.Level)
            {
                level_lb.text = "Max Lv";
               // btn_upgrade.interactable = false;

            }
            else
            {
                level_lb.text = "Lv " + dataGun.level.ToString();
                cf_next_level = ConfigManager.instance.configWeaponLevel.GetRecordByKeySearch(cf.ID, dataGun.level+1);
                cost_upgrade.text = cf_next_level.Cost.ToString();
       
                btn_upgrade.interactable = cf_next_level.Cost <= gold;
            }
        }
        else
        {
            level_lb.text = "Lv 1";
            //check buy
            cost_buy.text = cf.Cost.ToString();
            btn_buy.interactable = gold >= cf.Cost;
            cf_level = ConfigManager.instance.configWeaponLevel.GetRecordByKeySearch(cf.ID,1);
        }
        damage_e.Setup(cf_level.Damage, cf_level_max.Damage);
        rage_e.Setup((int)cf_level.Range, (int)cf_level_max.Range);
        rof_e.Setup(cf_level.ROF,cf_level_max.ROF);
        clip_e.Setup(cf_level.Clipsize, cf_level_max.Clipsize);
    }
    private void UpdateSlot(object data)
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

    public void OnClose()
    {
        DialogManager.instance.HideDialog(dialogIndex);
    }

    public void OnEquip()
    {
        DialogManager.instance.ShowDialog(DialogIndex.DialogConfirmEquip, new DialogConfirmEquipParam { config = cf});
    }

    public void OnBuy()
    {
        DataAPIController.instance.BuyWeapon(cf);
        var data = (object)cf;
        QuestManager.instance.LogQuest(new QuestLogData { questType = QuestType.BUY_GUN , data = data });
    }

    public void OnUpgrade()
    {
        DataAPIController.instance.UpgradeWeapon(cf, (res) =>
        {
            Debug.Log("upgrade " + res);
            var data = (object)cf;
            QuestManager.instance.LogQuest(new QuestLogData { questType = QuestType.UPGRADE_GUN, data = data });
        });
    }

    public override void HideDialog()
    {
        DataTrigger.UnRegisterValueChange(DataPath.GUNS + "/" + cf.ID.ToKey(), SetInfo);
        DataTrigger.UnRegisterValueChange(DataPath.SLOT, UpdateSlot);
        DataTrigger.UnRegisterValueChange(DataPath.GOLD, UpdateGold);
    }
}
