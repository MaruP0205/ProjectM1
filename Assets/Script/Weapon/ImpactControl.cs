using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactControl : MonoBehaviour
{
    public float life_Time = 2;
    public NamePool namePool;
    public void OnSpawn()
    {
        Invoke("OnDelayDeSpawn", life_Time);
    }

    private void OnDelayDeSpawn()
    {
        PoolManager.instance.DeSpawn(namePool, transform);
    }

    public void OnDeSpawn()
    {

    }
}
