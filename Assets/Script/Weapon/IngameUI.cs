using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

public class IngameUI : BYSingletonMono<IngameUI>
{
    [SerializeField]
    private WeaponControl weaponControl;
    public Transform btn_fire;
    public Text nameGun;
    public Image gun_icon;
    public RectTransform parentEnemyHub;
    public Image hp_progress;
    public Text hp_lb;
    public Image wave_progress;
    public Text wave_lb;
    // Start is called before the first frame update
    void Start()
    {
        weaponControl.OnChangeGun += OnChangeGun;
        MissionManager.instance.OnWaveChange += OnWaveChange;
        CharacterControl character = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControl>();
        character.OnHPChange += OnHPChange;
    }

    private void OnHPChange(int arg1, int arg2)
    {
        hp_lb.text = arg1.ToString()+"/"+arg2.ToString();
        float val = (float)arg1 / (float)arg2;
        hp_progress.fillAmount = val;
    }

    private void OnWaveChange(int arg1, int arg2)
    {
        wave_lb.text = arg1.ToString()+"/"+arg2.ToString();
        float val = (float)arg1 / (float)arg2;
        wave_progress.fillAmount = val; 

    }

    private void OnChangeGun(WeaponBehavior weapon)
    {
       nameGun.text = weapon.data.cf_weapon.Name;
        gun_icon.overrideSprite = SpriteLiblaryControl.instance.GetSpriteByName(weapon.data.cf_weapon.Icon);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSwapGun()
    {
        weaponControl.ChangeGun();
    }
    public void OnFire(bool isFire)
    {
        btn_fire.localScale = isFire ? Vector3.one * 0.8f:Vector3.one;
        InputManager.OnFire(isFire);
        
    }

    public void OnPauseGame()
    {
        DialogManager.instance.ShowDialog(DialogIndex.DialogPause);
    }
}
