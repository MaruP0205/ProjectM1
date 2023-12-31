using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoardUI : MonoBehaviour
{
    public Transform camTransform;
    Quaternion originalRotation;
    void Start()
    {
        camTransform = Camera.main.transform;
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = camTransform.rotation * originalRotation;
    }
}
