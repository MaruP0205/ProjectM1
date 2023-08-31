using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;

public class BYBaseDataTable : ScriptableObject
{
    public virtual void CreateData(TextAsset text)
    {

    }
}
public class ConfigCompare<T> : IComparer<T> where T : class, new()
{
    private List<FieldInfo> keyfields = new List<FieldInfo>();
    public ConfigCompare(params string[] keyInfoNames)
    {
        foreach (string s in keyInfoNames)
        {
            FieldInfo fieldInfo = typeof(T).GetField(s,BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Instance);
            keyfields.Add(fieldInfo);
        }
    }
    public int Compare(T x, T y)
    {
        int result = 0;
        for (int i = 0; i < keyfields.Count; i++)
        {
            object val_x = keyfields[i].GetValue(x);
            object val_y = keyfields[i].GetValue(y);
            result = ((IComparable)val_x).CompareTo(val_y);
            if (result != 0)
                break;
        }
        return result;
    }
    public T SetValueSearch(params object[] vals)
    {
        T key =new T();
        for(int i = 0; i < vals.Length; i++)
        {
            keyfields[i].SetValue(key, vals[i]);
        }
        return key;
    }
}
public abstract class BYDataTable<T> : BYBaseDataTable where T : class, new()
{
    [SerializeField]
    protected List<T> records = new List<T>();
    private ConfigCompare<T> configCompare;
    public abstract ConfigCompare<T> DefindConfigCompare();
    private void OnEnable()
    {
        configCompare = DefindConfigCompare();
    }
    public override void CreateData(TextAsset text)
    {
        configCompare = DefindConfigCompare();
        records.Clear();
        List<List<string>> grids = SplitCSVFile(text);
        Type recordsType = typeof(T);
        FieldInfo[] fieldInfos = recordsType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        for (int n = 1; n < grids.Count; n++)
        {
            List<string> data_line = grids[n];
            string json = "{";
            for (int i = 0; i < fieldInfos.Length; i++)
            {
                if (i > 0)
                    json += ",";

                if (fieldInfos[i].FieldType != typeof(string))
                {
                    string dataField = "0";
                    if (data_line.Count > i)
                    {
                        if (data_line[i] != string.Empty)
                        {
                            dataField = data_line[i];
                        }
                    }
                    json += "\"" + fieldInfos[i].Name + "\":" + dataField;
                }
                else
                {
                    string dataField = string.Empty;
                    if (i < data_line.Count)
                    {
                        if (data_line[i] != string.Empty)
                        {
                            dataField = data_line[i];
                        }
                    }
                    json += "\"" + fieldInfos[i].Name + "\":\"" + dataField + "\"";
                }
            }
            json += "}";
            T r = JsonUtility.FromJson<T>(json);
            
            records.Add(r);
        }
        records.Sort(configCompare);
    }
    private List<List<string>> SplitCSVFile(TextAsset csvText)
    {
        List<List<string>> grids = new List<List<string>>();
        string[] lines = csvText.text.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            string s_Line = lines[i];
            if (s_Line.CompareTo(string.Empty) != 0)
            {
                string[] s_data = s_Line.Split('\t');
                List<string> ls_line = new List<string>();
                foreach (string s in s_data)
                {
                    string newChar = Regex.Replace(s, @"\t|\n|\r", "");
                    newChar = Regex.Replace(newChar, @"""", "" + "");
                    ls_line.Add(newChar);
                }
                grids.Add(ls_line);
            }


        }
        return grids;
    }
    public T GetRecordByKeySearch(params object[] keys)
    {
        T objectKeySearch = configCompare.SetValueSearch(keys);
        int index = records.BinarySearch(objectKeySearch, configCompare);
        if (index >= 0 && index < records.Count)
        {
            return records[index];

        }
        else { return null; }
    }

    public List<T> GetAllRecords()
    {
        return records;
    }
}

