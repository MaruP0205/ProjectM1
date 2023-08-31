using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimControl : MonoBehaviour
{
    public Transform aim_trans;
    private Transform trans;
    public LayerMask layerMask_;
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
        if (Input.GetMouseButton(0))
        {
            Vector3 pos_screen = Input.mousePosition;
            /* pos_screen.z = 54;
             Vector3 pos_World = Camera.main.ScreenToWorldPoint(pos_screen);
             aim_trans.position = pos_World; */
            Ray r = Camera.main.ScreenPointToRay(pos_screen);
            Debug.DrawRay(r.origin, r.direction *80, Color.red);
            RaycastHit hitInfo;
            if(Physics.Raycast(r, out hitInfo, 80, layerMask_))
            {
                aim_trans.position = hitInfo.point;
            }
        }
    }
}
