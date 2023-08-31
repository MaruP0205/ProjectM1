using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sniper : WeaponBehavior
{
    public int nps = 5;
    public AudioSource audioSource_;
    public AudioClip[] sfx_fire;
    public AudioClip sfx_ready;
    public override void Setup(GunData data)
    {
        base.Setup(data);
        iWeaponHandle_ = new ISniper();
        iWeaponHandle_.Setup(this);

        BYPool pool = new BYPool();
        pool.name_pool = NamePool.BL_SNIPER;
        pool.total = clipSize;
        pool.prefab = projecties_pf;
        PoolManager.instance.AddNewPool(pool);
    }
}

public class ISniper : IWeaponHandle
{
    private Sniper wp;
    public void FireHandle()
    {
        wp.audioSource_.PlayOneShot(wp.sfx_fire.OrderBy(x => Guid.NewGuid()).FirstOrDefault());

        Transform bl = PoolManager.instance.Spawn(NamePool.BL_SNIPER);
        bl.position = wp.muzzleFlash.transform.position;

        if (wp.characterControl.enemy_trans != null)
        {
            Vector3 pos_e = wp.characterControl.cur_enemy.transform.position;
            float dis = Vector3.Distance(bl.position, pos_e);
            if (dis <= 3) //ti le headshot
            {
                pos_e = wp.characterControl.cur_enemy.GetTargetShoot(BodyType.HEAD).position;
            }
            else
            {
                pos_e = wp.characterControl.cur_enemy.GetTargetShoot(BodyType.NORMAL).position;
            }
            Vector3 dir = pos_e - bl.position;
            dir.Normalize();
            bl.forward = dir;
        }
        else
        {
            bl.forward = wp.muzzleFlash.GetDirShoot();
        }
        bl.GetComponent<NewBulletControl>().Setup(new BulletData { damage = wp.damage, force = wp.force });

    }
    public void ReadyHandle()
    {
        wp.audioSource_.PlayOneShot(wp.sfx_ready);

    }
    public void Setup(WeaponBehavior wp)
    {
        this.wp = (Sniper)wp;
    }
}
