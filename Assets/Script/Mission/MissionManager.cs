using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MissionManager : BYSingletonMono<MissionManager>
{
    List<string> wave_ids;
    private int indexWave = -1;
    private bool isDoneCreate;
    private int numberEnemyDead;
    private int totalEnemyWave;
    public event Action<int, int> OnWaveChange;
    private ConfigMissionRecord cf;
    public void InitMission(ConfigMissionRecord cf)
    {
        this.cf = cf;
        wave_ids = cf.Waves;
        StartCoroutine("StartNewWave");
    }

    IEnumerator StartNewWave()
    {
        yield return new WaitForSeconds(1);
        indexWave++;
        if(indexWave >= wave_ids.Count)
        {
            //Victory
            DialogManager.instance.ShowDialog(DialogIndex.DialogWin, new dialogWinParam {  config = cf});
        }
        else
        {
            OnWaveChange?.Invoke(indexWave + 1,wave_ids.Count);
            isDoneCreate = false;
            numberEnemyDead = 0;
            ConfigWaveRecord cf_wave = ConfigManager.instance.configWave.GetRecordByKeySearch(wave_ids[indexWave]);
            totalEnemyWave = cf_wave.Number;
            List<string> enemy_ids = cf_wave.Enemies;
            for(int i = 0; i < cf_wave.Number;i++)
            {
                yield return new WaitForSeconds(cf_wave.Deplay);
                string id_enemy = enemy_ids.OrderBy(x =>Guid.NewGuid()).FirstOrDefault();
                ConfigEnemyRecord configEnemy = ConfigManager.instance.configEnemy.GetRecordByKeySearch(id_enemy);
                CreateEnemy(configEnemy);

            }
        }
        yield return null;
        isDoneCreate = true;
    }
    private void CreateEnemy(ConfigEnemyRecord cf)
    {
        GameObject go_enemy = Instantiate(Resources.Load("Enemy/"+cf.Prefab,typeof(GameObject))) as GameObject;
        Transform root_pos = SceneConfig.instance.GetSpawnPointRandom();
        go_enemy.transform.position = root_pos.position;
        go_enemy.GetComponent<EnemyControl>().Setup(new EnemyInitData { config = cf, root_pos = root_pos });
    }
    public void OnEnemyDead(EnemyControl enemy)
    {
        
        numberEnemyDead++;
        if(isDoneCreate)
        {
            if(numberEnemyDead >= totalEnemyWave)
            {
                StopCoroutine("StartNewWave");
                StartCoroutine("StartNewWave");
            }
        }
    }
}
