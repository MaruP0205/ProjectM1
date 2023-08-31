using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DialogIndex
{
    DialogConfirmShop = 1,
    DialogWeaponInfo = 2,
    DialogConfirmEquip = 3,
    DialogFail=4,
    DialogMissionInfo=5,
    DialogPause=6,
    DialogWin=7,

}

public class DialogParam
{

}

public class DialogConfirmShopParam: DialogParam
{
    public ConfigShopRecord config;
}

public class DialogWeaponInfoParam : DialogParam
{
    public ConfigWeaponRecord config;
    public ItemData itemData = null;
}
public class DialogMissionInfoParam : DialogParam
{
    public ConfigMissionRecord config;
    
}

public class DialogConfirmEquipParam : DialogParam
{
    public ConfigWeaponRecord config;
}

public class dialogWinParam : DialogParam
{
    public ConfigMissionRecord config;
    public int star = 1;
}
public class DialogConfig
{
    public static DialogIndex[] dialogIndices = {
        DialogIndex.DialogConfirmShop,
        DialogIndex.DialogWeaponInfo,
        DialogIndex.DialogConfirmEquip,
        DialogIndex.DialogFail,
        DialogIndex.DialogMissionInfo,
        DialogIndex.DialogPause,
        DialogIndex.DialogWin,
    };
}
