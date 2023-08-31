using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOnDamage : MonoBehaviour
{
    public BodyType bodyType;
    private EnemyControl parent;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        parent = gameObject.GetComponentInParent<EnemyControl>();
    }
    public void OnDamage(BulletData bulletData)
    {
        bulletData.bodyType = bodyType;
        bulletData.rb = rb;
        parent.OnDamage(bulletData);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
