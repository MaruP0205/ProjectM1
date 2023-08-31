using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleGeneric<M>
{   
    public M sample_val;
    public void ShowInfo(M val)
    {
        Debug.Log("var: "+val.ToString());
        Debug.Log("sample var: " + sample_val.ToString());
    }
}
