using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : BYSingletonMono<ConfigManager>
{
    [NonSerialized]
    public ConfigShop configShop;
    [NonSerialized]
    public ConfigWeapon configWeapon;
    [NonSerialized]
    public ConfigWeaponLevel configWeaponLevel;
    [NonSerialized]
    public ConfigEnemy configEnemy;
    [NonSerialized]
    public ConfigMission configMission;
    [NonSerialized]
    public ConfigWave configWave;
    [NonSerialized]
    public ConfigStage configStage;
    [NonSerialized]
    public ConfigQuest configQuest;
    public void Init(Action callback)
    {
        StartCoroutine(WaitInit(callback));
    }
    // Start is called before the first frame update
    IEnumerator WaitInit(Action callback)
    {
        configShop = Resources.Load("Config/ConfigShop", typeof(ScriptableObject)) as ConfigShop;
        yield return new WaitUntil(() => configShop != null);
        configWeapon = Resources.Load("Config/ConfigWeapon", typeof(ScriptableObject)) as ConfigWeapon;
        yield return new WaitUntil(() => configWeapon != null);
        configWeaponLevel = Resources.Load("Config/ConfigWeaponLevel", typeof(ScriptableObject)) as ConfigWeaponLevel;
        yield return new WaitUntil(() => configWeaponLevel != null);
        configEnemy = Resources.Load("Config/ConfigEnemy", typeof(ScriptableObject)) as ConfigEnemy;
        yield return new WaitUntil(() => configEnemy != null);
        configWave = Resources.Load("Config/ConfigWave", typeof(ScriptableObject)) as ConfigWave;
        yield return new WaitUntil(() => configWave != null);
        configMission = Resources.Load("Config/ConfigMission", typeof(ScriptableObject)) as ConfigMission;
        yield return new WaitUntil(() => configMission != null);
        configStage = Resources.Load("Config/ConfigStage", typeof(ScriptableObject)) as ConfigStage;
        yield return new WaitUntil(() => configStage != null);
        configQuest = Resources.Load("Config/ConfigQuest", typeof(ScriptableObject)) as ConfigQuest;
        yield return new WaitUntil(() => configQuest != null);
        yield return null;
        callback?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
