using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class NewEnemyUnit : Enemy
{
    enum unit_states
    {
        none,
        hide,
        hidetoact,
        act,
        actpause,
        acttodie,
        die
    }
    StateMachine<unit_states> unit_state = null;

    protected EnemyAttackAura aura;

    // timer to coroutine
    protected float delta_time
    {
        get
        {
            if(unit_state.GetCurrentState() != unit_states.act)
                return 0;
            return Time.deltaTime;
        }
    }

    protected bool is_acting
    {
        get
        {
            return unit_state.GetCurrentState() == unit_states.act;
        }
    }

    // this should be called before its sibling
    public virtual void Start()
    {
        // initialize state machine
        unit_state = new StateMachine<unit_states>();
        unit_state.AddState(unit_states.none, () => { });
        unit_state.AddState(unit_states.act, UnitStateAct);
        unit_state.AddState(unit_states.actpause, UnitStateActPause);
        unit_state.SetInitState(unit_states.act);


        // add aura
        GameObject aura_object = (GameObject)Instantiate(Resources.Load("Prefabs/EnemyAura"), transform.position, new Quaternion());
        aura = aura_object.GetComponent<EnemyAttackAura>();
        aura.SetOwner(this);
    }

    // this should be called before its sibling
    public virtual void Update()
    {
        unit_state.UpdateState();
    }

    // this should be called before its sibling
    public virtual void LateUpdate()
    {
        unit_state.LateUpdateState();
    }


    void UnitStateHide()
    {

    }

    void UnitStateHideToAct()
    {

    }

    void UnitStateAct()
    {
        aura.damage = damage;
        aura.SetAuraSize(AuraRange);
    }

    void UnitStateActPause()
    {

    }

    void UnitStateActToDie()
    {
    }

    void UnitStateDie()
    {

    }

    protected abstract void OnDamaged(float damage);
    protected abstract void OnKnockBack(Vector3 vector);
    protected abstract void OnStun(float time);
    protected abstract void OnSnare(float time);

    protected abstract void OnPause();
    protected abstract void OnResume();
    protected abstract void OnDie();

    public override void giveDamage(float damage)
    {
        if (unit_state.GetCurrentState() == unit_states.act)
            OnDamaged(damage);
    }
    public override void giveKnockback(Vector3 moveVector)
    {
        if (unit_state.GetCurrentState() == unit_states.act)
            OnKnockBack(moveVector);
    }

    public override void giveStun(float time)
    {
        if (unit_state.GetCurrentState() == unit_states.act)
            OnStun(time);
    }

    public override void giveSnare(float time)
    {
        if (unit_state.GetCurrentState() == unit_states.act)
            OnSnare(time);
    }

    public override void pause()
    {
        OnPause();
        unit_state.ChangeState(unit_states.actpause);
    }

    public override void resume()
    {
        OnResume();
        unit_state.ChangeState(unit_states.act);
    }
    public override void Die()
    {
        // create dead body
        OnDie();
        GameManager.I.EXPIncrease(giveEXP, transform.position);
        Destroy(transform.parent.gameObject);

    }
}
