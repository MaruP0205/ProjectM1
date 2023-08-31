using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    [SerializeField]
    private WeaponControl weaponControl;
    public Transform targer;
    private Transform trans;
    public Camera cam;
    private void Awake()
    {
        trans = transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        weaponControl.OnChangeGun += OnChangeGun;
    }

    private void OnChangeGun(WeaponBehavior weapon)
    {
        cam.fieldOfView = weapon.fov;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        //trans.position = new Vector3(-2,2,targer.position.z) ;
        trans.position = targer.position;
    }
}
