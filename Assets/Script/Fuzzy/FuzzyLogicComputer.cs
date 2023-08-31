using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FuzzyLogicComputer : MonoBehaviour
{
    public AnimationCurve low;
    public AnimationCurve medium;
    public AnimationCurve Hight;
    // gioi han gia trij de tinh
    public float target_low;
    public float target_medium;
    public float target_hight;
    protected float lowValue=0;
    protected float mediumValue;
    protected float hightValue;
    // Start is called before the first frame update
    public abstract float EvaluateStatements(float inputValuePercent);
   
}
