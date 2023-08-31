using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : BYSingletonMono<GameManager>
{
    public const float length_BG_E = 16f;
    public const int total_E = 6;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CreateMission(ConfigMissionRecord cf)
    {
        GameObject go = Instantiate(Resources.Load("Mission/MissionManager",typeof(GameObject))) as GameObject;
        go.GetComponent<MissionManager>().InitMission(cf);
        GameObject player = Instantiate(Resources.Load("Logic", typeof(GameObject))) as GameObject;
    }
}
