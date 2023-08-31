using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class SceneConfig : BYSingletonMono<SceneConfig>
{
    public List<Transform> spawnPoints;
    public Transform Pos_Player;    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform GetSpawnPointRandom()
    {
        /*int index = UnityEngine.Random.Range(0, spawnPoints.Count);
         * return spawnPoints[index];
         */
        return spawnPoints.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
    }
}
