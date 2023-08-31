using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shotgun : WeaponBehavior
{
    public int nps = 5;
    public AudioSource audioSource_;
    public AudioClip[] sfx_fire;
    public AudioClip sfx_ready;
    public override void Setup(GunData data)
    {
        base.Setup(data);
        iWeaponHandle_ = new IShotgun();
        iWeaponHandle_.Setup(this);
        BYPool pool = new BYPool();
        pool.name_pool = NamePool.BL_SHOOTGUN;
        pool.total = nps * 3;
        pool.prefab = projecties_pf;
        PoolManager.instance.AddNewPool(pool);

        
    }
}

public class IShotgun : IWeaponHandle
{
    private Shotgun wp;
    public void FireHandle()
    {
        wp.audioSource_.PlayOneShot(wp.sfx_fire.OrderBy(x => Guid.NewGuid()).FirstOrDefault());
        for (int i=0; i<wp.nps; i++)
        {
            float x = UnityEngine.Random.Range(-4f, 4f);
            float y = UnityEngine.Random.Range(-1f, 1f);
            Transform bl = PoolManager.instance.Spawn(NamePool.BL_SHOOTGUN);
            bl.position = wp.muzzleFlash.transform.position;
            Quaternion q = Quaternion.Euler(x, y, 0);
            if (wp.characterControl.cur_enemy != null)
            {
                Vector3 dir = wp.characterControl.cur_enemy.transform.position - bl.position;
                dir.Normalize();
                bl.forward = q*dir;
            }
            else
            {
                bl.forward = q*  wp.muzzleFlash.GetDirShoot();

            }
            bl.GetComponent<NewBulletControl>().Setup(new BulletData {damage = wp.damage, force =wp.force });
        }
    }
    public void ReadyHandle()
    {
        wp.audioSource_.PlayOneShot(wp.sfx_ready);

    }
    public void Setup(WeaponBehavior wp)
    {
        this.wp = (Shotgun)wp;
    }
}
