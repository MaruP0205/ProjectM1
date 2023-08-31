using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleBuffer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadInGame(int id)
    {
        ConfigMissionRecord cf = ConfigManager.instance.configMission.GetRecordByKeySearch(id);
        LoadSceneManager.instance.LoadSceneByName(cf.SceneName, () =>
        {
            GameManager.instance.CreateMission(cf);
        });
    }
}
