using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIRootControlScale : MonoBehaviour
{
    [SerializeField] CanvasScaler[] canvasScalers;
    public float rate;
    // Start is called before the first frame update
    void Start()
    {
        rate = 1920f / 1080f;
        float currentRate = (float)Screen.width / (float)Screen.height;
        float scale = currentRate > rate ? 1 : 0;
        foreach (CanvasScaler e in canvasScalers)
        {
            e.matchWidthOrHeight = scale;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
