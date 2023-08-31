using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PoolManager : BYSingletonMono<PoolManager>
{
    public List<BYPool> default_Pools;
    private Dictionary<NamePool, BYPool> dicPool = new Dictionary<NamePool, BYPool>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (BYPool pool in default_Pools)
        {
            AddNewPool(pool);
        }
    }

    public void AddNewPool(BYPool pool)
    {
        for(int i = 0; i < pool.total; i++)
        {
           
            Transform trans = Instantiate(pool.prefab);
            trans.gameObject.SetActive(false);
            pool.items.Add(trans);
        }
        dicPool.Add(pool.name_pool, pool);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform Spawn(NamePool pool_name)
    {
        BYPool pool = dicPool[pool_name];
        return pool.Spawn();
    }

    public void DeSpawn(NamePool pool_name,Transform trans)
    {
        BYPool pool = dicPool[pool_name];
        pool.DeSpawn(trans);
    }

    internal Transform Spawn(object namePool_ip)
    {
        throw new NotImplementedException();
    }
}
