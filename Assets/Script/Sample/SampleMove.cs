using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleMove : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform trans;
    [SerializeField] private float speedMove = 0.1f;

    private void Awake()
    {
        trans = transform;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 point_dir = new Vector3(x, 0, z);
        if (point_dir.magnitude > 0)
            trans.forward = point_dir;
        Vector3 pos = trans.position;

        pos = pos + point_dir.normalized * speedMove;
        trans.position = pos;
    }

    private void FixedUpdate()
    {

    }

}
