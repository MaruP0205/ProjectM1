using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Singleton<T> where T : class,new()
{
    private static readonly T singleton = new T();

    public static T instance
    {
        get
        {
            
            return singleton; 
        
        }
        private set
        {

        }
    }
}
public class BYSingletonMono<T>: MonoBehaviour where T : MonoBehaviour
{
    private static T singleton;
    public static T instance
    {
        get
        {

            if (BYSingletonMono<T>.singleton==null)
            {
                BYSingletonMono<T>.singleton = (T)FindObjectOfType(typeof(T));
                if (BYSingletonMono<T>.singleton==null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "[@"+typeof(T).Name+"]";
                    BYSingletonMono<T>.singleton = obj.AddComponent<T>();
                }
            }

            return BYSingletonMono<T>.singleton;

        }
        private set
        {

        }


    }
    private void Reset()
    {
        gameObject.name = typeof(T).Name;
    }

}
