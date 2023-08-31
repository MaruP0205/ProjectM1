using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuzzyBraveSoldier : FuzzyLogicComputer
{
   

    // Start is called before the first frame update
    void Start()
    {
        float res = EvaluateStatements(0.875f);
        Debug.LogError(res);
    }

    public override float EvaluateStatements(float inputValuePercent)
    {
        hightValue = Hight.Evaluate(inputValuePercent);
        mediumValue = medium.Evaluate(inputValuePercent);
        lowValue = low.Evaluate(inputValuePercent);
        float val = -1;
        val = (hightValue * target_low + mediumValue * target_medium + lowValue * target_hight) / (hightValue + mediumValue + lowValue);
        Debug.Log(val);
        return val / target_hight;
    }
}
