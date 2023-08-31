using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopViewItem : MonoBehaviour
{
    public Text name_lb;
    public Text value_lb;
    public Text cost_lb;
    public Image icon;
    public int id;
    private ConfigShopRecord cf;
    // Start is called before the first frame update
    public void Setup(ConfigShopRecord cf)
    {
        this.cf = cf;
        name_lb.text = cf.Name;
        value_lb.text = cf.Value.ToString();
        cost_lb.text = cf.Cost.ToString()+ "$";
        //icon.overrideSprite = Resources.Load("Icon/"+cf.Description, typeof(Sprite)) as Sprite;
        icon.overrideSprite = SpriteLiblaryControl.instance.GetSpriteByName(cf.Description);
        icon.SetNativeSize();
    }

    private void Start()
    {
        if(id > 0)
        {
            ConfigShopRecord cf = ConfigManager.instance.configShop.GetRecordByKeySearch(id);
            this.cf = cf;
            name_lb.text = cf.Name;
            value_lb.text = cf.Value.ToString();
            cost_lb.text = cf.Cost.ToString() + "$";
           // icon.overrideSprite = Resources.Load("Icon/"+cf.Description, typeof(Sprite)) as Sprite;
            icon.overrideSprite = SpriteLiblaryControl.instance.GetSpriteByName(cf.Description);
            icon.SetNativeSize();
        }
       
    }

    public void OnBuy()
    {
        DialogManager.instance.ShowDialog(DialogIndex.DialogConfirmShop, new DialogConfirmShopParam { config = cf});
    }
}
