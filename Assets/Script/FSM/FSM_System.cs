using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_System : MonoBehaviour
{
    public FSM_State current_state;
    public void GotoState(FSM_State newState)
    {
        if (current_state != null)
        {
            current_state.Exit();

        }
        current_state = newState;
        current_state.OnEnter();
    }
    public void GotoState(FSM_State newState, object data)
    {
        if (current_state != null)
        {
            current_state.Exit();

        }
        current_state = newState;
        current_state.OnEnter(data);
    }

    public virtual void LateUpdate()
    {
        current_state?.OnLateUpdate();


    }

    public virtual void Update()
    {
        current_state?.OnUpdate();
    }

    public virtual void FixedUpdate()
    {
        current_state?.OnFixedUpdate();
    }

    #region Anim Event
    public virtual void OnAnimEnter()
    {
        current_state?.OnAnimEnter();
    }
    public virtual void OnAnimMiddle()
    {
        current_state?.OnAnimMiddle();
    }
    public virtual void OnAnimEnd()
    {
        current_state?.OnAnimEnd();
    }
    #endregion
}
