using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControl : BYSingletonMono<MenuControl> 
{
    public GameObject homeView_info;
    public GameObject shopView_info;
    public GameObject gunView_info;
    public GameObject missionView_info;
    public GameObject questView_info;
    public Text level_lb;
    public Text exp_lb;
    public Image level_pg;
    public Text gold_lb;
    public GameObject topbar;

    private void Awake()
    {
        SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
    }

    private void SceneManager_sceneUnloaded(Scene arg0)
    {
        if(arg0.buildIndex == 0)
        {
           
            int gold = DataAPIController.instance.GetGold();
            gold_lb.text = gold.ToString();
            DataTrigger.RegisterValueChange(DataPath.GOLD, GoldValueChange);
            PlayerInfo info = DataAPIController.instance.GetInfo();
            level_lb.text = "LV"+info.level.ToString();
            exp_lb.text = info.exp.ToString();
        }
    }

    private void GoldValueChange(object data)
    {
        int gold = (int)data;
        gold_lb.text = gold.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
        ViewManager.instance.OnPreViewSiwtch += OnViewSwitch;

    }

    private void OnViewSwitch(ViewIndex obj)
    {
        switch (obj)
        {
            case ViewIndex.HomeView:
                topbar.SetActive(true); 
                homeView_info.SetActive(true);
                shopView_info.SetActive(false);
                missionView_info.SetActive(false);
                gunView_info.SetActive(false);
                questView_info.SetActive(false);
                break;

            case ViewIndex.ShopView:
                topbar.SetActive(true);
                homeView_info.SetActive(false);
                shopView_info.SetActive(true);
                missionView_info.SetActive(false);
                gunView_info.SetActive(false);
                questView_info.SetActive(false);
                break;

            case ViewIndex.WeaponView:
                topbar.SetActive(true);
                homeView_info.SetActive(false);
                shopView_info.SetActive(false);
                missionView_info.SetActive(false);
                gunView_info.SetActive(true);
                questView_info.SetActive(false);
                break;

            case ViewIndex.QuestView:
                topbar.SetActive(true);
                homeView_info.SetActive(false);
                shopView_info.SetActive(false);
                missionView_info.SetActive(false);
                gunView_info.SetActive(false);
                questView_info.SetActive(true);
                break;

            case ViewIndex.MissionView:
                topbar.SetActive(false);
                
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBack()
    {
        ViewManager.instance.SwitchView(ViewIndex.HomeView);
    }

    public void OnSetting()
    {

    }

    public void OnShop()
    {
        DialogManager.instance.HideAllDialog();
        ViewManager.instance.SwitchView(ViewIndex.ShopView);
    }
}
