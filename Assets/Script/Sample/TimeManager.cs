using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : BYSingletonMono<TimeManager>
{
    
    public void ShowTime()
    {
        DateTime dateTime = DateTime.Now;
        Debug.Log(dateTime.DayOfWeek);
    }
}

public class TimeManager_T: Singleton<TimeManager_T>
{
    public void ShowTime()
    {
        DateTime dateTime = DateTime.Now;
        Debug.Log(dateTime.TimeOfDay);
    }
}

public class ConvertString: Singleton<ConvertString>
{
    public string Upcase(string mess)
    {
        string s = mess.ToUpper();
        return s;
    }
}
