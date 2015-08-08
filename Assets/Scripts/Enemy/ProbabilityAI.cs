using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewRangeAI : NewEnemyUnit {


    // states
    protected enum ai_states
    {
        none,
        idle, // not find target
        trace, // move to target
        attack,
        special_0 // knockback, stun, snare
    }
    protected StateMachine<ai_states> ai_state = null;
    private bool is_paused = false;

    // Actions
    [SerializeField]
    public List<EnemyAction> attacks;
    [SerializeField]
    public List<EnemyAction> traces = null;

    EnemyAction current_action = null;
    EnemyAction next_action = null;


    // Components
    public EnemyAnimation anim = null;


	// Use this for initialization
	void Start () 
    {
        base.Start();

        // initialize state machine
        ai_state = new StateMachine<ai_states>();
        ai_state.AddState(ai_states.none, () => { });
        ai_state.AddState(ai_states.idle, OnIdleState);
        ai_state.AddState(ai_states.trace, OnTracingState);
        ai_state.AddState(ai_states.attack, OnAttackState);
        ai_state.AddState(ai_states.special_0, OnSpecial0State);
        ai_state.SetInitState(ai_states.idle);

        // get component
        anim = GetComponent<EnemyAnimation>();
        if (anim == null)
        {
            Debug.LogError("Error On Finding Internal EnemyAnimation Script");
            Application.Quit();
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        base.Update();
        if (!is_acting)
            return;

        ai_state.UpdateState();
	}

    void LateUpdate()
    {
        base.LateUpdate();

        ai_state.LateUpdateState();

        current_action = next_action;
    }




    private void OnIdleState()
    {
        if (ai_state.IsFirstFrame())
        {
        }

        anim.applyState(STATE_MONSTER.IDLE);
        StateChangeOnIdle();
        
    }

    private void OnTracingState()
    {
        if (ai_state.IsFirstFrame())
        {
            current_action.OnStart();
        }

        current_action.Act();

        if (current_action.isEnd() && IsAIStateChangeable())
        {
            StateChangeDefault();
        }
    }

    private void OnAttackState()
    {
        if (ai_state.IsFirstFrame())
        {
            current_action.OnStart();
        }

        current_action.Act();

        if (current_action.isEnd() && IsAIStateChangeable())
        {
            StateChangeDefault();
        }
    }

    private void OnSpecial0State()
    {

        StateChangeDefault();
    }


    private bool IsAIStateChangeable()
    {
        if (ai_state.GetNextState() == ai_states.special_0)
            return false;
        return true;
    }

    private void StateChangeOnIdle()
    {
        EnemyAction action = FindAvailableAction(traces);
        if (action != null && IsAIStateChangeable())
        {
            next_action = action;
            ai_state.ChangeState(ai_states.trace);

            if (current_action != null)
                current_action.OnStop();
        }
    }

    private void StateChangeDefault()
    {
        // check attack available
        EnemyAction action = FindAvailableAction(attacks);
        if (action != null)
        {
            next_action = action;
            ai_state.ChangeState(ai_states.attack);

            if (current_action != null)
                current_action.OnStop();

            return;
        }

        // check tracking available
        action = FindAvailableAction(traces);
        if (action != null)
        {
            next_action = action;
            ai_state.ChangeState(ai_states.trace);

            if (current_action != null)
                current_action.OnStop();

            return;
        }

        // next state should be idle
        next_action = null;
        ai_state.ChangeState(ai_states.idle);

        if (current_action != null)
            current_action.OnStop();
    }

    private void StateChangeToSpecial()
    {
        ai_state.ChangeState(ai_states.special_0);

        if (current_action != null)
            current_action.OnStop();
    }

    protected override void OnDamaged(float damage)
    {
        currentHP -= damage;
    }

    protected override void OnKnockBack(Vector3 vector)
    {
        //apply knockback

        StateChangeToSpecial();
    }

    protected override void OnStun(float time)
    {
        //apply stun

        StateChangeToSpecial();
    }

    protected override void OnSnare(float time)
    {
        //apply snare

        StateChangeToSpecial();
    }

    protected override void OnPause()
    {
        is_paused = true;
    }

    protected override void OnResume()
    {
        is_paused = false;
    }

    protected override void OnDie()
    {
        if (Debug.isDebugBuild)
        {
            Debug.Log("Range Enemy Died : " + gameObject.name);
        }
    }
}
