using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundElement : MonoBehaviour
{
    public int index;
    [SerializeField] private Transform trans_player;
    // Start is called before the first frame update
    void Start()
    {
        trans_player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z + GameManager.length_BG_E*2 < trans_player.position.z)
        {
            //gameObject.SetActive(false);
            index = index + GameManager.total_E;
            transform.position = new Vector3(0, 0, index * GameManager.length_BG_E);
        }
    }
}
