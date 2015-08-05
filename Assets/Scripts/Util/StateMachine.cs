using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public delegate void FUNC_STATE_UPDATE();

public class StateMachine
{
    private bool is_init_statemachine = true;

    private Dictionary<System.Object, FUNC_STATE_UPDATE> state_func_map = new Dictionary<object,FUNC_STATE_UPDATE>();
    private System.Object current_state = null;
    private System.Object next_state = null;
    private bool is_first_frame = true;
    private bool is_state_change = true;


    public void UpdateState()
    {
        is_init_statemachine = false;
        state_func_map[current_state]();
    }

    public void LateUpdateState()
    {
        current_state = next_state;
        is_first_frame = false;
        if (is_state_change)
            is_first_frame = true;
        is_state_change = false;
    }

    public void AddState(System.Object state, FUNC_STATE_UPDATE update_function)
    {
        state_func_map.Add(state, update_function);
    }

    public void SetInitState(System.Object state)
    {
        if (is_init_statemachine)
            current_state = state;
    }

    public void ChangeState(System.Object _next_state)
    {
        next_state = _next_state;
        is_state_change = true;
    }

    public System.Object GetCurrentState()
    {
        return current_state;
    }

    public bool IsFirstFrame()
    {
        return is_first_frame;
    }
};
