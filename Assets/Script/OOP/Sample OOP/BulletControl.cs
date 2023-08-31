using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletControl : MonoBehaviour
{
    private int damage;
    public float force = 5;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Fire(int damage)
    {
        this.damage = damage;
        gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * force);
    }
    public void Fire(BulletData bulletData)// 
    {
        this.damage = bulletData.damage;
        gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * bulletData.force);
    }
    private void OnCollisionExit(Collision collision)
    {

    }

    private void OnCollisionStay(Collision collision)
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        
        EnemyControl e = collision.gameObject.GetComponent<EnemyControl>();
        if (e != null)
        {
            //e.OnDamage(damage);
        }
        PoolManager.instance.DeSpawn(NamePool.BULLET, transform); 
    }

    private void OnTriggerEnter(Collider other)
    {

    }

    public void OnSpawn()
    {
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public void OnDeSpawn()
    {
     //   Debug.Log("bullet despawn");
    }
}
