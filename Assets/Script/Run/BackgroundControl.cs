using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundControl : MonoBehaviour
{
    [SerializeField] private List<Transform> bg_Elements;
    [SerializeField] private float timer;
    // Start is called before the first frame update
    void Start()
    {
        for(int i= 0; i < bg_Elements.Count; i++)
        {
            bg_Elements[i].position = new Vector3(0, 0 , i * GameManager.length_BG_E);
            bg_Elements[i].GetComponent<BackgroundElement>().index = i;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
