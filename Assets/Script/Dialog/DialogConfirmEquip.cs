using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

public class DialogConfirmEquip : BaseDialog
{
    public Image iconGun_1;
    public Image iconGun_2;
    private DialogConfirmEquipParam dl_param;

    public override void Setup(DialogParam param)
    {
        base.Setup(param);
        dl_param =(DialogConfirmEquipParam)param;
        List<int> ls = DataAPIController.instance.GetSlotEquip();
        ConfigWeaponRecord cf_1 = ConfigManager.instance.configWeapon.GetRecordByKeySearch(ls[0]);
        iconGun_1.overrideSprite = SpriteLiblaryControl.instance.GetSpriteByName(cf_1.Icon);
        ConfigWeaponRecord cf_2 = ConfigManager.instance.configWeapon.GetRecordByKeySearch(ls[1]);
        iconGun_2.overrideSprite = SpriteLiblaryControl.instance.GetSpriteByName(cf_2.Icon);

    }

    public void OnSlot_1()
    {
        DataAPIController.instance.OnChangeSlot(0, dl_param.config.ID, (res) => 
        {
            DialogManager.instance.HideDialog(dialogIndex);

        });
    }

    public void OnSlot_2()
    {
        DataAPIController.instance.OnChangeSlot(1, dl_param.config.ID, (res) =>
        {
            DialogManager.instance.HideDialog(dialogIndex);

        });
    }

    public void OnClose()
    {
        DialogManager.instance.HideDialog(dialogIndex);
    }
}
