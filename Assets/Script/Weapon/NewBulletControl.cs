using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BulletData
{
    public Vector3 dir;
    public Vector3 point;
    public int damage;
    public float force;
    public WeaponType weaponType;
    public BodyType bodyType;
    public Rigidbody rb;
}

public class NewBulletControl : MonoBehaviour
{
    public float speed = 10;
    public float life_Time = 2;
    private Transform trans;
    public NamePool namePool;
    public NamePool namePool_ip_flesh;
    public NamePool namePool_ip_metal;
    public LayerMask mask;
    private BulletData data;
    // Start is called before the first frame update
    void Awake()
    {
        trans = transform;
    }

    public void Setup(BulletData bulletData)
    {
        data = bulletData;
    }
    private void Update()
    {
        trans.Translate(Vector3.forward * Time.deltaTime * speed);
        RaycastHit hit;
        if(Physics.SphereCast(trans.position,0.05f, trans.forward,out hit, 1, mask))
        {
            PoolManager.instance.DeSpawn(namePool, trans);
            Transform impact = null;
            if (hit.collider.gameObject.CompareTag(GameConfig.Tag_Flesh))
            {
                impact = PoolManager.instance.Spawn(namePool_ip_flesh);
                data.dir = trans.forward;
                data.point = hit.point;
                EnemyOnDamage enemyOnDamage = hit.collider.GetComponent<EnemyOnDamage>();
                enemyOnDamage?.OnDamage(data);
            }
            else if (hit.collider.gameObject.CompareTag(GameConfig.Tag_Metal))
            {
                impact = PoolManager.instance.Spawn(namePool_ip_metal);
            }
            if(impact != null)
            {
                impact.position = hit.point;
                impact.forward = hit.normal;
            }
            
        }
    }
    
    public void OnSpawn()
    {
        Invoke("OnDelayDeSpawn", life_Time);
    }

    private void OnDelayDeSpawn()
    {
        PoolManager.instance.DeSpawn(namePool,trans);
    }

    public void OnDeSpawn()
    {
        
    }
}
