using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ConfigWaveRecord
{
    [SerializeField]
    private string id;
    [SerializeField]
    private int number;
    public int Number { get { return number; } }
    [SerializeField]
    private string enemies;
    public List<string> Enemies 
    { 
        get 
        {
            string[] s = enemies.Split(';');
            List<string> ls = new List<string>();
            ls.AddRange(s);
            return ls; 
        } 
    }
    [SerializeField]
    private float deplay;
    public float Deplay { get { return deplay; } }
}
public class ConfigWave : BYDataTable<ConfigWaveRecord>
{
    public override ConfigCompare<ConfigWaveRecord> DefindConfigCompare()
    {
        return new ConfigCompare<ConfigWaveRecord>("id");
    }
}
