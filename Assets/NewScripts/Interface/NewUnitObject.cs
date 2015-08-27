using UnityEngine;
using System.Collections;

public class NewUnitObject : PauseAbleObject {

	public float HPMax = 100;
	public float HPCurrent = 100;
	public float Speed = 7;
	public float Damage = 3;
	public float AuraRange = 5;
	public int Exp = 0;
	public bool CanBeKnockBacked = true;
	public bool CanBeSnared = true;
	public bool CanBeStunned = true;

	private bool isDead = false;
	private SpriteRenderer renderer = null;
	private Rigidbody rigid = null;


	protected enum states {
		IDLE,
		REVEAL,
		ACT, ACTSTOPPED,
		DIE,
		DEAD
	};
	protected StateMachine<System.Object> mvStateMachine;


	// Use this for initialization
	protected override void OnStart() {
		// initialize statemachine
		mvStateMachine = new StateMachine<System.Object>();
		mvStateMachine.AddState(states.IDLE, () => { });
		mvStateMachine.AddState(states.REVEAL, OnStateReveal);
		mvStateMachine.AddState(states.ACT, OnStateAct);
		mvStateMachine.AddState(states.ACTSTOPPED, OnStateActStop);
		mvStateMachine.AddState(states.DIE, OnStateDie);
		mvStateMachine.AddState(states.DEAD, OnStateDead);
		mvStateMachine.SetInitState(states.IDLE);

		renderer = GetComponent<SpriteRenderer>();
		if (renderer == null) {
			if(Debug.isDebugBuild)
				Debug.Log(name + "No Renderer Found");
		}

		rigid = GetComponent<Rigidbody>();
		if (rigid == null) {
			if (Debug.isDebugBuild)
				Debug.Log(name + "No RigidBody Found");
		}
	}
	
	// Update is called once per frame
	protected  override void OnUpdate() {
		mvStateMachine.UpdateState();
	}

	protected override void OnLateUpdate() {
		if ((states)mvStateMachine.GetCurrentState() == states.ACT) {
			if (HPCurrent <= 0)
				mvStateMachine.ChangeState(states.DIE);
		}
		mvStateMachine.LateUpdateState();
	}


	protected virtual void OnStateReveal() {
		const float revealing_time = 1;
		if(mvStateMachine.IsFirstFrame()){
			StartCoroutine(RevealTimer(revealing_time));

			Color initcolor = renderer.color;
			initcolor.a = 0;
			renderer.color = initcolor;
		}

		Color tmp = renderer.color;
		tmp.a += GetDeltaTime() / revealing_time;
		renderer.color = tmp;
	}

	IEnumerator RevealTimer(float time) {
		var timer = DelayedTimer.WaitForCustomDeltaTime(time, GetDeltaTime);
		yield return StartCoroutine(timer);

		mvStateMachine.ChangeState(states.ACT);
	}

	// this should be implemented on child
	protected virtual void OnStateAct() {
		if (mvStateMachine.IsFirstFrame()) {
			Color tmp = renderer.color;
			tmp.a = 1;
			renderer.color = tmp;
		}
		//do nothing
	}

	protected virtual void OnStateDie() {
		const float dieing_time = 1;
		if (mvStateMachine.IsFirstFrame()) {
			StartCoroutine(DieTimer(dieing_time));
		}

		Color tmp = renderer.color;
		tmp.a -= GetDeltaTime() / dieing_time;
		renderer.color = tmp;
	}

	IEnumerator DieTimer(float time) {
		var timer = DelayedTimer.WaitForCustomDeltaTime(time, GetDeltaTime);
		yield return StartCoroutine(timer);

		mvStateMachine.ChangeState(states.DEAD);
	}

	protected virtual void OnStateActStop() {

	}

	protected virtual void OnStateDead() {
		NewGameManager.Inst.OnUnitDied(transform.parent.gameObject, this);
	}


	public virtual void GiveDamage(float amount) {
		HPCurrent -= amount;
	}

	public virtual void GiveStun(float time) {
		if (!CanBeStunned)
			return;
		
		mvStateMachine.ChangeState(states.ACTSTOPPED);

		ActStop(time);
		StopAura(time);
	}

	public virtual void GiveKnockBack(Vector3 vector) {
		if (!CanBeKnockBacked)
			return;

		mvStateMachine.ChangeState(states.ACTSTOPPED);

		rigid.AddForce(vector);
		ActStop(0.2f);
		KinematicOff(0.2f);
	}

	public virtual void GiveSnare(float time, float rate) {
		if (!CanBeSnared)
			return;

		GiveSnare(time, rate);
	}


	// act stop
	IEnumerator actstoproutine = null;
	void ActStop(float time) {
		if (actstoproutine != null) {
			StopCoroutine(actstoproutine);
		}

		actstoproutine = ActStopTimer(time);
		StartCoroutine(actstoproutine);
	}

	IEnumerator ActStopTimer(float time) {
		var timer = DelayedTimer.WaitForCustomDeltaTime(time, GetDeltaTime);
		yield return StartCoroutine(timer);

		mvStateMachine.ChangeState(states.ACT);
		actstoproutine = null;
	}

	// snare
	IEnumerator snareroutine = null;
	private float prevSpeed = 0;
	void Snare(float time, float rate) {
		if (snareroutine != null) {
			StopCoroutine(snareroutine);
			Speed = prevSpeed;
		}

		
		snareroutine = SnareTimer(time, rate);
		StartCoroutine(snareroutine);
	}

	IEnumerator SnareTimer(float time, float rate) {
		prevSpeed = Speed;
		Speed *= (1 - rate);

		var timer = DelayedTimer.WaitForCustomDeltaTime(time, GetDeltaTime);
		yield return StartCoroutine(timer);

		Speed = prevSpeed;
		snareroutine = null;
	}
	

	// stop aura
	IEnumerator stopauraroutine = null;
	private float prevAuraRange = 0;
	void StopAura(float time) {
		if (stopauraroutine != null) {
			StopCoroutine(stopauraroutine);
			AuraRange = prevAuraRange;
		}

		stopauraroutine = StopAuraTimer(time);
		StartCoroutine(stopauraroutine);
	}

	IEnumerator StopAuraTimer(float time) {
		prevAuraRange = AuraRange;
		AuraRange = 0;

		var timer = DelayedTimer.WaitForCustomDeltaTime(time, GetDeltaTime);
		yield return StartCoroutine(timer);

		AuraRange = prevAuraRange;
		stopauraroutine = null;
	}

	// kinematic off
	IEnumerator kinematicroutine = null;
	void KinematicOff(float time) {
		if (kinematicroutine != null) {
			StopCoroutine(kinematicroutine);
		}

		kinematicroutine = KinematicTimer(time);
		StartCoroutine(kinematicroutine);
	}

	IEnumerator KinematicTimer(float time) {
		yield return new WaitForFixedUpdate();
		rigid.isKinematic = true;
        yield return StartCoroutine(DelayedTimer.WaitForCustomDeltaTime(time, GetDeltaTime));

		yield return new WaitForFixedUpdate();
		rigid.isKinematic = false;

		kinematicroutine = null;
	}


	protected override void OnPauseAnimation() {
		// pause animation

	}

	protected override void OnResumeAnimation() {
		// pause animation

	}


	public void Spawn() {
		if (Debug.isDebugBuild)
			Debug.Log("Spawning" + name);
		mvStateMachine.ChangeState(states.REVEAL);
	}

	public void GiveExp(int amount) {
		Exp += amount;
	}
}
