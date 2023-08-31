using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class SampleNavAgent : MonoBehaviour
{
    private Transform trans;
    // Start is called before the first frame update
    public NavMeshAgent agent;
    public NavMeshPath path;
    private int index = -1;
    private Vector3 cur_pos;
    //52:30
    IEnumerator Start()
    {
        path = new NavMeshPath();
        trans = transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        
        agent.Warp(trans.position);
        agent.destination = trans.position;

        yield return new WaitForSeconds(1);
        agent.updatePosition = false;

        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                //agent.destination = hit.point;
                agent.Warp(trans.position);
                agent.CalculatePath(hit.point, path);
                if (path.status != NavMeshPathStatus.PathInvalid )
                {
                    if(path.corners.Length > 1)
                    {
                        StopCoroutine("LoopWalk");
                        StartCoroutine("LoopWalk");
                    }
                }
            }
        }
       // UpdateRotation();
        
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < path.corners.Length - 1; i++)
        {

        }
    }

    private void UpdateRotation()
    {
        Vector3 Pos_Steering = agent.steeringTarget;
        Vector3 dir = Pos_Steering - trans.position;
        if (dir.magnitude > 0)
        {
            dir.Normalize();
            Quaternion q = Quaternion.LookRotation(dir, Vector3.up);
            trans.rotation = Quaternion.Slerp(trans.rotation, q, Time.deltaTime * 360);
        }
        

    }
    IEnumerator LoopWalk()
    {
        WaitForSeconds wait = new WaitForSeconds(0.02f);
        index = 1;
        while (index<path.corners.Length)
        {
            cur_pos = path.corners[index];
            float dis = Vector3.Distance(trans.position, cur_pos);
            if (dis <= 0.3f)
            {
                index++;
               
            }
            else
            {
                Vector3 dir = cur_pos - trans.position;
                if (dir.magnitude > 0)
                {
                    dir.Normalize();
                    Quaternion q = Quaternion.LookRotation(dir, Vector3.up);
                    trans.rotation = Quaternion.Slerp(trans.rotation, q, Time.deltaTime * 360);
                }
                trans.position = Vector3.MoveTowards(trans.position, cur_pos, Time.deltaTime * 50);
            }
            yield return wait;
        }
        agent.Warp(trans.position);
    }
}
