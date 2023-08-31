using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogConfirmShop : BaseDialog
{
    public Text name_lb;
    public Text value_lb;
    public Text cost_lb;
    public Image icon;
    private DialogConfirmShopParam dl_param; 
    public override void Setup(DialogParam param)
    {
        base.Setup(param);
        dl_param = (DialogConfirmShopParam)param;
        name_lb.text = dl_param.config.Name;
        value_lb.text = dl_param.config.Value.ToString();
        cost_lb.text = dl_param.config.Cost.ToString() + "$";
        //icon.overrideSprite = Resources.Load("Icon/"+cf.Description, typeof(Sprite)) as Sprite;
        icon.overrideSprite = SpriteLiblaryControl.instance.GetSpriteByName(dl_param.config.Description);
        icon.SetNativeSize();
    }

    public void OnClose()
    {
        DialogManager.instance.HideDialog(dialogIndex);
    }

    public void OnBuy()
    {
        DataAPIController.instance.BuyGold(dl_param.config, () =>
        {
            DialogManager.instance.HideDialog(dialogIndex);
        });
       
    }
}
