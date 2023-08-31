using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [HideInInspector] public Transform trans;
    public float speed = 1f;
    public Animator animator;
    private void Awake()
    {
        trans = transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            trans.position = new Vector3(trans.position.x - 1,0, trans.position.z);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            trans.position = new Vector3(trans.position.x + 1, 0, trans.position.z);
        }
        trans.position = trans.position + Vector3.forward * (speed * Time.deltaTime);
       
    }
}
