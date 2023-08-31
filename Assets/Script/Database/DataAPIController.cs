using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataAPIController : BYSingletonMono<DataAPIController>
{
    [SerializeField]
    private DataModel dataModel;
    public void InitData(Action callback)
    {
        dataModel.InitData(() =>
        {
            CheckDailyLogin();

            callback();
        });
    }
    public void CheckDailyLogin()
    {

        DateTime timelastLogin = dataModel.ReadData<DateTime>(DataPath.TIME_LOGIN);
        DateTime time_Now = DateTime.UtcNow;
        if (timelastLogin.Year < time_Now.Year)
        {
            // reset
            ResetDaialyData();
        }
        else
        {
            if (timelastLogin.DayOfYear < time_Now.DayOfYear)
            {
                // reset 
                ResetDaialyData();
            }
        }
    }
    private void ResetDaialyData()
    {
        Debug.LogError("ResetDaialyData");
        dataModel.UpdateData(DataPath.TIME_LOGIN, DateTime.UtcNow);
        Dictionary<string, QuestData> dic = new Dictionary<string, QuestData>();
        dataModel.UpdateData(DataPath.DIC_QUEST, dic);

    }
    public PlayerInfo GetInfo()
    {
        
        return dataModel.ReadData<PlayerInfo>(DataPath.INFO);
        

      
    }

    public int GetGold()
    {
        int gold = dataModel.ReadData<int>(DataPath.GOLD);

        return gold;
    }

    public void BuyGold(ConfigShopRecord configShopRecord, Action callback)
    {
        int cur_gold = GetGold();
        cur_gold += configShopRecord.Value;
        dataModel.UpdateData(DataPath.GOLD, cur_gold,callback);
    }

    #region Weapon
    public void BuyWeapon(ConfigWeaponRecord configWeaponRecord)
    {
        int cur_gold = GetGold();
        if(cur_gold >= configWeaponRecord.Cost)
        {
            cur_gold -= configWeaponRecord.Cost;
            dataModel.UpdateData(DataPath.GOLD, cur_gold);
            ItemData itemData = new ItemData();
            itemData.id = configWeaponRecord.ID;
            itemData.level = 1;
            dataModel.UpdateDataDictionary<ItemData>(DataPath.GUNS, configWeaponRecord.ID.ToKey(), itemData, null);
        }
        
    }
    public void UpgradeWeapon(ConfigWeaponRecord configWeaponRecord, Action<bool> callback)
    {
        ItemData itemData = dataModel.ReadDictionary<ItemData>(DataPath.GUNS, configWeaponRecord.ID.ToKey());
        
        if(itemData != null )
        {
            ConfigWeaponLevelRecord cf_level = ConfigManager.instance.configWeaponLevel.GetRecordByKeySearch(configWeaponRecord.ID, itemData.level);
            ConfigWeaponLevelRecord cf_next_level;
            ConfigWeaponLevelRecord cf_level_max = ConfigManager.instance.configWeaponLevel.GetMaxLevel(configWeaponRecord.ID);
            if (cf_level.Level < cf_level_max.Level)
            {
                cf_next_level = ConfigManager.instance.configWeaponLevel.GetRecordByKeySearch(configWeaponRecord.ID, itemData.level+1);
                int cur_gold = GetGold();
                if (cur_gold >= cf_next_level.Cost)
                {
                    cur_gold -= cf_next_level.Cost;
                    dataModel.UpdateData(DataPath.GOLD, cur_gold);
                    itemData.level += 1;
                    dataModel.UpdateDataDictionary<ItemData>(DataPath.GUNS, configWeaponRecord.ID.ToKey(), itemData, null);
                    callback?.Invoke(true);
                }
                else
                {
                    callback?.Invoke(false);
                }
            }
            else
            {
                callback?.Invoke(false);
            }
            
        }
        else
        {
            callback?.Invoke(false);
        }
    }

    public ItemData GetWeaponData(int idGun)
    {
        ItemData itemData = dataModel.ReadDictionary<ItemData>(DataPath.GUNS, idGun.ToKey());
        return itemData;
    }

    public List<int> GetSlotEquip()
    {
        return dataModel.ReadData<List<int>>(DataPath.SLOT);
    }

    public int GetSlot(int id)
    {
        List<int> ls = GetSlotEquip();
        int index = ls.IndexOf(id);
        return index;
    }

    public void OnChangeSlot(int slot, int id_weapon,Action<bool> callback)
    {
        List<int> ls= GetSlotEquip();
        int id_WP_Slot = ls[slot];
        bool isSwap = false;
        if (ls[0] == id_weapon || ls[1] == id_weapon)
        {
            isSwap = true;
        }

        if(id_weapon == id_WP_Slot)
        {
            callback(false);
        }
        else
        {
            if (isSwap)
            {

                if (slot == 0)
                {
                    int id_swap = ls[0];
                    ls[0] = id_weapon;
                    ls[1] = id_swap;
                }
                else
                {
                    int id_swap = ls[1];
                    ls[1] = id_weapon;
                    ls[0] = id_swap;
                }
            }
            else
            {
                ls[slot] = id_weapon;
                
            }
            dataModel.UpdateData(DataPath.SLOT, ls);
            callback(true);

        }
    }
    #endregion

    #region Mission
    public void UpdateMission(dialogWinParam param,out int reward)
    {
        reward = param.config.Reward;
        int id_cur = dataModel.ReadData<int>(DataPath.CUR_MISSION);
        if(id_cur == param.config.ID)
        {
            if(param.star > 0)
            {
                MissionData missionData = new MissionData();
                missionData.id = param.config.ID;
                missionData.star = param.star;
                dataModel.UpdateDataDictionary<MissionData>(DataPath.DIC_MISSION, param.config.ID.ToKey(), missionData);
                id_cur++;
                dataModel.UpdateData(DataPath.CUR_MISSION, id_cur);
            }
            
        }
        else
        {
            reward = (int)(param.config.Reward*0.5f);
            MissionData mission_data = dataModel.ReadDictionary<MissionData>(DataPath.CUR_MISSION, param.config.ID.ToKey());
            if(mission_data.star < param.star)
            {
                mission_data.star = param.star;
                dataModel.UpdateDataDictionary<MissionData>(DataPath.DIC_MISSION, param.config.ID.ToKey(), mission_data);

            }

        }
        int cur_gold = GetGold();
        cur_gold += reward;
        dataModel.UpdateData(DataPath.GOLD, cur_gold);
    }

    public MissionData GetMissionData(ConfigMissionRecord cf)
    {
        return dataModel.ReadDictionary<MissionData>(DataPath.DIC_MISSION, cf.ID.ToKey());
    }

    public ConfigMissionRecord GetCurrentMission()
    {
        int id_cur = dataModel.ReadData<int>(DataPath.CUR_MISSION);
        return ConfigManager.instance.configMission.GetRecordByKeySearch(id_cur);
    }

    public int GetTotalStarMission()
    {
        Dictionary<string, MissionData> dic = dataModel.ReadData<Dictionary<string, MissionData>>(DataPath.DIC_MISSION);
        return dic.Sum(x => x.Value.star);
    }
    #endregion
    #region Quest
    public QuestData GetQuestData(int id)
    {
        QuestData data = dataModel.ReadDictionary<QuestData>(DataPath.DIC_QUEST,id.ToKey());
        if(data == null)
        {
            data = new QuestData();
            data.id = id;
            data.is_claimed = false;
            data.number = 0;
            dataModel.UpdateDataDictionary<QuestData>(DataPath.DIC_QUEST, id.ToKey(),data);
        }
        return data;
    }

    public void UpdateQuestData(int id)
    {
        QuestData data = dataModel.ReadDictionary<QuestData>(DataPath.DIC_QUEST, id.ToKey());
        if (data == null)
        {
            data = new QuestData();
            data.id = id;
            data.is_claimed = false;
            data.number = 0;
        }
        data.number++;
        dataModel.UpdateDataDictionary<QuestData>(DataPath.DIC_QUEST, id.ToKey(), data);
    }

    public void ClaimQuest(ConfigQuestRecord cf, Action<bool> callback)
    {
        QuestData data = dataModel.ReadDictionary<QuestData>(DataPath.DIC_QUEST, cf.ID.ToKey());
        if (data == null)
        {
            callback(false);
        }
        else
        {
            if(data.number < cf.Number)
            {
                callback(false);
            }
            else
            {
                int cur_gold = GetGold();
                cur_gold += cf.Reward;
                dataModel.UpdateData(DataPath.GOLD, cur_gold);
                data.is_claimed=true;
                dataModel.UpdateDataDictionary<QuestData>(DataPath.DIC_QUEST, cf.ID.ToKey(), data);
                callback(true); 
            }
        }
        
    }
    #endregion
}
