using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OgreControl : EnemyControl
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Setup(EnemyInitData enemyInitData)
    {
        base.Setup(enemyInitData);
    }
    public void jumping()
    {

    }
    public override void OnDamage(BulletData bulletData)
    {
        base.OnDamage(bulletData);
        hp -= bulletData.damage;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
