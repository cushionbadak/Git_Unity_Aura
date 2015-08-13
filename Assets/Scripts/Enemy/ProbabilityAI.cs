﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProbabilityAI : NewEnemyUnit {


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
    private bool is_state_changed = false;
    private float state_timer = 0;

    // Actions
    [SerializeField]
    public List<EnemyAction> attacks;
    [SerializeField]
    public List<EnemyAction> traces = null;

    EnemyAction current_action = null;
    EnemyAction next_action = null;


    // Components
    public EnemyAnimation anim = null;
    private int attack_num = 0;

    // variable bools
    public bool can_be_stunned = true;
    public bool can_be_snared = true;
    public bool can_be_knockbacked = true;

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

        // stop action on state change
        if (is_state_changed)
        {
            if (current_action != null)
            {
                if (current_action == next_action)
                    current_action.OnRestart();
                else
                    current_action.OnStop();
            }
        }

        current_action = next_action;
        is_state_changed = false;

    }


    // ============= //
    // State Methods //
    // ============= //

    private void OnIdleState()
    {
        if (ai_state.IsFirstFrame())
        {
        }

        anim.applyState(STATE_MONSTER.IDLE);
        StateChangeDefault(false, true, false);
        
    }

    private void OnTracingState()
    {
        if (ai_state.IsFirstFrame())
        {
            current_action.OnStart();
        }

        current_action.Act();
        anim.applyState(STATE_MONSTER.RUN);

        if (current_action.isEnd())
        {
            StateChangeDefault(true, true, true);
        }
    }

    private void OnAttackState()
    {
        if (ai_state.IsFirstFrame())
        {
            current_action.OnStart();
        }

        current_action.Act();
        switch (attack_num)
        {
            case 0:
                anim.applyState(STATE_MONSTER.ATTACK1);
                break;
            case 1:
                anim.applyState(STATE_MONSTER.ATTACK2);
                break;
            case 2:
                anim.applyState(STATE_MONSTER.ATTACK3);
                break;
        }
        if (current_action.isEnd())
        {
            StateChangeDefault(true, true, true);
        }
    }

    private void OnSpecial0State()
    {
        state_timer -= GetDeltaTime();

        if (state_timer <= 0)
        {
            aura.resume();
            StateChangeDefault(false, true, true);
        }
    }


    private bool IsAIStateChangeable()
    {
        if (ai_state.GetNextState() == ai_states.special_0)
            return false;
        return true;
    }

    private void StateChangeDefault(bool attack, bool trace, bool idle)
    {
        // check attack available
        if (attack)
        {
            EnemyAction action = FindAvailableAction(attacks);
            if (action != null)
            {
                ChangeState(ai_states.attack, action);
                for (int i = 0; i < attacks.Count - 1; ++i)
                {
                    if (action == attacks[i])
                    {
                        attack_num = i;
                        break;
                    }
                }

                return;
            }
        }

        // check tracking available
        if (trace)
        {
            EnemyAction action = FindAvailableAction(traces);
            if (action != null)
            {
                ChangeState(ai_states.trace, action);
                return;
            }
        }

        // check idle available
        if (idle)
        {
            ChangeState(ai_states.idle, null);
            return;
        }
    }

    private void StateChangeSpecial()
    {
        ChangeState(ai_states.special_0, null);
        foreach (var action in traces)
            action.OnReset();
        foreach (var action in attacks)
            action.OnReset();
    }

    private void ChangeState(ai_states next_state, EnemyAction action)
    {
        next_action = action;
        ai_state.ChangeState(next_state);
        is_state_changed = true;
    }


    // ================= //
    // Overrided Methods //
    // ================= //
    protected override void OnDamaged(float damage)
    {
        currentHP -= damage;
    }

    protected override void OnKnockBack(Vector3 vector)
    {
        if (can_be_knockbacked)
        {
            float knockbacked_time = 0.2f;
            //apply knockback
            gameObject.GetComponent<Rigidbody>().AddForce(vector);
            state_timer = knockbacked_time;
            StateChangeSpecial();

            StartCoroutine(TurnOffKinematic(knockbacked_time));
        }
    }

    protected override void OnStun(float time)
    {
        if (can_be_stunned)
        {
            //apply stun
            state_timer = time;
            StateChangeSpecial();

            StartCoroutine(TurnOffAura(time));
        }
    }

    protected override void OnSnare(float time)
    {
        if (can_be_snared)
        {
            //apply snare
            state_timer = time;
            StateChangeSpecial();
        }
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


    IEnumerator TurnOffAura(float time)
    {
        float backup_aura_range = AuraRange;
        AuraRange = 0;
        aura.StopAura();

        yield return StartCoroutine(DelayedTimer.WaitForCustomDeltaTime(time, GetDeltaTime));

        AuraRange = backup_aura_range;
        aura.ResumeAura();
    }

    IEnumerator TurnOffKinematic(float time)
    {
        //Kinematic을 켰다 켜 무한히 튕겨나가지 않도록 한다.
        yield return new WaitForFixedUpdate();
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        yield return StartCoroutine(DelayedTimer.WaitForCustomDeltaTime(time, GetDeltaTime));

        yield return new WaitForFixedUpdate();
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }

    float GetDeltaTime()
    {
        if (!is_acting)
            return 0;
        return Time.deltaTime;
    }

    protected EnemyAction FindAvailableAction(List<EnemyAction> list)
    {
        if (list.Count == 0)
            return null;

        List<EnemyAction> act_list = new List<EnemyAction>();
        int cost_sum = 0;
        foreach (var act in list)
        {
            if (act.isAvailable())
            {
                cost_sum += act.GetProbCost();
                act_list.Add(act);
            }
        }

        // tracking available
        if (act_list.Count != 0)
        {
            // 1 ~ cost_sum
            int rand = Random.Range(1, cost_sum + 1);
            foreach (var act in act_list)
            {
                rand -= act.GetProbCost();
                if (rand <= 0)
                    return act;
            }
        }

        return null;
    }
}
