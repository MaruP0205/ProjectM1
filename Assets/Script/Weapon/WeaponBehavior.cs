using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GunData
{
    public CharacterControl characterControl;
    public CharacterDataBinding dataBinding;
    public ConfigWeaponRecord cf_weapon;

}

public class WeaponBehavior : MonoBehaviour
{
    public IWeaponHandle iWeaponHandle_;
    public int damage = 5;
    public string nameGun;
    public float force;
    public float fov;
    public float range; 
    public float rof;
    public int clipSize;
    public float acurraycy;
    public float timeReload;
    private int number_bl;
    private float timeFire;
    public AnimatorOverrideController overrideController_;
    private CharacterDataBinding dataBinding;
    public MuzzleFlash muzzleFlash;
    public Transform projecties_pf;
    private bool isReady = false;
    public ConfigWeaponRecord cf_weapon;
    public GunData data;
    public CharacterControl characterControl;

    public virtual void Setup(GunData data)
    {
        this.data = data;
        this.dataBinding = data.dataBinding;
        this.cf_weapon = data.cf_weapon;
        this.characterControl = data.characterControl;


    }

    public virtual void OnDrawGun() 
    {
        this.dataBinding.animator.runtimeAnimatorController = overrideController_;
        this.dataBinding.Draw();
        StopCoroutine("MuzzleFlashSetup");
        StartCoroutine("MuzzleFlashSetup");
        iWeaponHandle_.ReadyHandle();
    }

    public virtual void OnHideGun()
    {
        isReady = false;
        muzzleFlash.Hide();
    }
    IEnumerator MuzzleFlashSetup()
    {
        yield return new WaitForSeconds(1.1f);
        muzzleFlash.Ready();
        isReady = true;
    }
    public void Update()
    {
        timeFire += Time.deltaTime;
        if (InputManager.isFire)
        {
            if (timeFire >= rof)
            {
                timeFire = 0;
                this.dataBinding.Fire();
                muzzleFlash.Fire();
                iWeaponHandle_.FireHandle();
            }
        }
    }
}
