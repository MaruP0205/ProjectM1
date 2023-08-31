using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretControl : MonoBehaviour
{
    public Transform aim_trans;
    public Transform base_trans;
    public Transform canon_trans;
    public BulletControl bullet_Prefab;
    public Transform posShoot;
    public Transform prefabBullet;
    // Start is called before the first frame update
    void Start()
    {
        BYPool pool_bl = new BYPool(prefabBullet,NamePool.BULLET,5);
        PoolManager.instance.AddNewPool(pool_bl);
        string s1 = "hello";
        string s = ConvertString.instance.Upcase(s1);
        TimeManager.instance.ShowTime();
        Debug.Log("Tu"+s1+"thanh"+s);
    }

    // Update is called once per frame
    void Update()
    {
        // base
        Vector3 posAim = aim_trans.position;
        posAim.y = base_trans.position.y;
        Vector3 dir_base = posAim - base_trans.position;
        base_trans.forward = dir_base.normalized;

        //c1;
        posAim = aim_trans.position;

        // posAim.x = canon_trans.position.x;
        Vector3 dir_canon = posAim - canon_trans.position;
        dir_canon.Normalize();
        //canon

        canon_trans.forward = dir_canon;
        //c2: 
        // Quaternion q = Quaternion.LookRotation(dir_canon.normalized, Vector3.up);
        // canon_trans.localRotation = q;

        Shoot();
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Transform bullet_trans = PoolManager.instance.Spawn(NamePool.BULLET);

            BulletControl bl = bullet_trans.GetComponent<BulletControl>();
            bl.transform.position = posShoot.position;
            bl.transform.forward = canon_trans.forward;
            //  bl.Fire(5);
            BulletData bulletData = new BulletData();
            bulletData.damage = 5;
            bulletData.force = 1000;
            bulletData.weaponType = WeaponType.HAND_GUN;

            bl.Fire(bulletData);

            //  bl.Fire(new BulletData { damage = 5, force = 1000, weaponType = WeaponType.SNIPER });
        }
    }
}
