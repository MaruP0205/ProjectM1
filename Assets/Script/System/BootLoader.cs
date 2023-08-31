using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootLoader : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        DontDestroyOnLoad(gameObject);
        yield return new WaitForSeconds(0.2f);
        ConfigManager.instance.Init(InitConfigDone);
    }
    private void InitConfigDone()
    {
        DataAPIController.instance.InitData(InitQuest);
        
    }
    private void InitQuest()
    {
        QuestManager.instance.InitQuest(() =>
        {
            QuestManager.instance.LogQuest(new QuestLogData { questType = QuestType.LOG_IN });
            LoadSceneManager.instance.LoadSceneByIndex(1, () =>
            {
                ViewManager.instance.SwitchView(ViewIndex.HomeView);
            });
        });
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
