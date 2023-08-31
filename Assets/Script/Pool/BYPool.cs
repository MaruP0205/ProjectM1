using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BYPool
{
    public Transform prefab;
    public NamePool name_pool;
    public int total;
    [HideInInspector]
    public List<Transform> items = new List<Transform>();
    private int index = -1;
    public BYPool()
    {

    }
    public BYPool(Transform prefab, NamePool namepool, int total)
    {
        this.prefab = prefab;
        this.name_pool = namepool;
        this.total = total;
    }

    public Transform Spawn()
    {
        index++;
        if(index >= items.Count)
        {
            index = 0;
        }
        Transform trans = items[index];
        trans.gameObject.SetActive(true);
        trans.SendMessage("OnSpawn",null,SendMessageOptions.DontRequireReceiver);
        return trans;
    }

    public void DeSpawn(Transform trans)
    {
        trans.SendMessage("OnDeSpawn", null, SendMessageOptions.DontRequireReceiver);
        trans.gameObject.SetActive(false);
        
    }
}
