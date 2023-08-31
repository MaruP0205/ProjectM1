using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Reflection;
using UnityEngine.UI;
using UnityEngine.Events;

public static class DataTrigger
{
    /// <summary>
    /// Custum Extention method convert path to list path
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static List<string> ConvertToListPath(this string path) //extenion method convert path to list path
    {
        string[] s = path.Split('/');
        List<string> paths = new List<string>();
        paths.AddRange(s);
        return paths;
    }
    private static Dictionary<string, UnityEvent<object>> dic_valueChange = new Dictionary<string, UnityEvent<object>>();
    public static void RegisterValueChange(string path, UnityAction<object> delegateDataChange)
    {
        if (!dic_valueChange.ContainsKey(path))
        {
            dic_valueChange[path] = new UnityEvent<object>();
        }
        dic_valueChange[path].AddListener(delegateDataChange);
    }
    public static void UnRegisterValueChange(string path, UnityAction<object> delegateDataChange)
    {
        if (dic_valueChange.ContainsKey(path))
        {
            dic_valueChange[path].RemoveListener(delegateDataChange);
        }
    }
    public static void TriggerValueChange(this string path, object data)
    {
        if (dic_valueChange.ContainsKey(path))
        {
            dic_valueChange[path].Invoke(data);
        }
    }

    public static string ToKey(this int id)
    {
        return "K_"+id.ToString();
    }

    public static int FromKey(this string key)
    {
        string[] s = key.Split('_');
        return int.Parse(s[1]);
    }
}
public class DataModel : MonoBehaviour
{
    private PlayerData playerData;
    public void InitData(Action callback)
    {
        if (loadData())
        {
            callback?.Invoke();
            Debug.Log("Load DAta");
        }
        else
        {
            Debug.Log("Create New");
            //create new Data
            playerData = new PlayerData();
            PlayerInfo info = new PlayerInfo();
            info.name = "hello";
            info.level = 1;
            info.exp = 0;
            List<int> ls = new List<int>();
            ls.Add(1);
            ls.Add(2);
            info.guns_equip = ls;
            playerData.info = info;
            MapInfo mapInfo = new MapInfo();
            mapInfo.currentMission = 1;
            mapInfo.dic_Mission = new Dictionary<string, MissionData>();
            playerData.mapInfo = mapInfo;
            PlayerInventory playerInventory = new PlayerInventory();
            playerInventory.gold = 100;
            Dictionary<string, ItemData> dic_gun = new Dictionary<string, ItemData>();
            dic_gun["K_1"] = new ItemData { id = 1,level = 1 };
            playerInventory.dic_guns = dic_gun;
            dic_gun["K_2"] = new ItemData { id = 2, level = 1 };
            playerInventory.dic_guns = dic_gun;
            playerData.inventory = playerInventory;
            saveData();
            callback?.Invoke();

        }
        
    }
    #region Read Normal 
    public T ReadData<T>(string path)
    {
        object outData;

        List<string> paths = path.ConvertToListPath();
       
        ReadDataByPath(paths, playerData, out outData);
        return (T)outData;
    }
    //info/name
    private void ReadDataByPath(List<string> paths, object data,out object outData)
    {
        outData = null;
        string p = paths[0];
        Type t = data.GetType();
        FieldInfo field = t.GetField(p);
        if(paths.Count == 1)
        {
            outData = field.GetValue(data);
        }
        else
        {
            paths.RemoveAt(0);
            ReadDataByPath(paths, field.GetValue(data), out outData);
        }
    }
    #endregion
    #region Read Dictionary
    public T ReadDictionary<T>(string path,string key)
    {
        List<string> paths = path.ConvertToListPath();
        T outData;
        ReadDataDictionaryByPath(paths,playerData,key,out outData);
        return outData;
    }

    private void ReadDataDictionaryByPath<T>(List<string> paths,object data, string key,out T dataOut)
    {
        string p = paths[0];
        Type t = data.GetType();
        FieldInfo field = t.GetField(p);
        if (paths.Count == 1)
        {
            object dic = field.GetValue(data);
            Dictionary<string, T> dicData = (Dictionary<string, T>)dic;
            dicData.TryGetValue(key, out dataOut);
        }
        else
        {
            paths.RemoveAt(0);
            ReadDataDictionaryByPath(paths, field.GetValue(data), key, out dataOut);
        }
    }
    #endregion
    #region Update Normal
    public void UpdateData(string path,object newData, Action callback=null)
    {
        
        List<string> paths = path.ConvertToListPath();
        UpdateDataByPath(paths, playerData, newData,callback);
        saveData();
        path.TriggerValueChange(newData);
        

    }

    private void UpdateDataByPath(List<string> paths,object data, object newData, Action callback)
    {
        string p = paths[0];
        Type t = data.GetType();
        FieldInfo field = t.GetField(p);
        if (paths.Count == 1)
        {
           // outData = field.GetValue(data);
           field.SetValue(data, newData);
            callback?.Invoke();
        }
        else
        {
            paths.RemoveAt(0);
            UpdateDataByPath(paths, field.GetValue(data),newData, callback);
        }
    }
    #endregion
    #region Update Dictionary
    public void UpdateDataDictionary<T>(string path, string key, T newData, Action callback=null)
    {
        List<string> paths = path.ConvertToListPath();

        object dicDataOut;
        UpdateDataDictionaryByPath<T>(paths, key, playerData, newData,out dicDataOut, callback);
        saveData();
        (path + "/" + key).TriggerValueChange(newData);
        (path).TriggerValueChange(dicDataOut);
    }

    private void UpdateDataDictionaryByPath<T>(List<string> paths, string key, object data, T newData, out object dataOut, Action callback)
    {
        string p = paths[0];
        Type t = data.GetType();
        FieldInfo field = t.GetField(p);
        if (paths.Count == 1)
        {
            //outData = field.GetValue(data);
            object dic = field.GetValue(data);
            Dictionary<string, T> dicData = (Dictionary<string, T>)dic;
            if (dicData == null)
            {
                dicData = new Dictionary<string, T>();
            }
            dicData[key] = newData;
            dataOut = dicData;
            field.SetValue(data, dicData);
            callback?.Invoke();
        }
        else
        {
            paths.RemoveAt(0);
            UpdateDataDictionaryByPath(paths, key, field.GetValue(data), newData, out dataOut, callback);
        }
    }
    #endregion
    private void saveData()
    {
        string json_string = JsonConvert.SerializeObject(playerData);
        Debug.Log(json_string);
        PlayerPrefs.SetString("DATA", json_string);
    }

    private bool loadData() 
    {
        if (PlayerPrefs.HasKey("DATA"))
        {
            string json_String = PlayerPrefs.GetString("DATA");
            playerData = JsonConvert.DeserializeObject<PlayerData>(json_String);
            return true;
        }
        return false;
    }
}
