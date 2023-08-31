using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class FSM_Animation_State : StateMachineBehaviour
{
    private FSM_System system;
    private float timeCount = 0;
    public float middleTime;
    private bool isPlay = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (system == null)
        {
            system = animator.GetComponentInParent<FSM_System>();
        }
        timeCount = 0;
        isPlay = false;
        system.OnAnimEnter();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        system.OnAnimEnter();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeCount += Time.deltaTime;
        if(timeCount >= middleTime&&!isPlay)
        {
            isPlay = true;
            system.OnAnimMiddle();
        }
    }
}
