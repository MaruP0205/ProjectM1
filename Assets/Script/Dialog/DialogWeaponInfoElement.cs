using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogWeaponInfoElement : MonoBehaviour
{
    private float maxSize = 377;
    public Image progressImage;
    public Text val;
    public void Setup(int min, int max)
    {
        val.text = min.ToString()+"/"+max.ToString();
        float x = ((float)min/(float)max)*maxSize;
        progressImage.rectTransform.sizeDelta = new Vector2(x, progressImage.rectTransform.sizeDelta.y); 
    }

    public void Setup(float min, float max)
    {
        val.text = min.ToString() + "/" + max.ToString();
        float x = ((float)min / (float)max) * maxSize;
        progressImage.rectTransform.sizeDelta = new Vector2(x, progressImage.rectTransform.sizeDelta.y);
    }
}
