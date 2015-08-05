using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public delegate void FUNC_STATE_UPDATE();

public class StateMachine<T>
{
    private bool is_init_statemachine = true;

    private Dictionary<T, FUNC_STATE_UPDATE> state_func_map = new Dictionary<T,FUNC_STATE_UPDATE>();
    private T prev_state = default(T);
    private T current_state = default(T);
    private T next_state = default(T);
    private bool is_first_frame = true;
    private bool is_state_change = false;


    public void UpdateState()
    {
        is_init_statemachine = false;
        state_func_map[current_state]();
    }

    public void LateUpdateState()
    {
        if (is_state_change)
        {
            prev_state = current_state;
            current_state = next_state;
            next_state = default(T);
        }

        is_first_frame = false;
        if (is_state_change)
            is_first_frame = true;
        is_state_change = false;
    }

    public void AddState(T state, FUNC_STATE_UPDATE update_function)
    {
        state_func_map.Add(state, update_function);
    }

    public void SetInitState(T state)
    {
        if (is_init_statemachine)
            current_state = state;
    }

    public void ChangeState(T _next_state)
    {
        next_state = _next_state;
        is_state_change = true;
    }

    public T GetPrevState()
    {
        return prev_state;
    }

    public T GetCurrentState()
    {
        return current_state;
    }

    public T GetNextState()
    {
        return next_state;
    }

    public bool IsFirstFrame()
    {
        return is_first_frame;
    }
};
