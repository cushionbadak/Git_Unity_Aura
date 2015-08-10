using UnityEngine;
using System.Collections;

public class MeleeAI : MonoBehaviour, EnemyAIInterface{
    // Animation script
    EnemyAnimation anim;

	// states
	enum states {
		idle,
		move,
		rest,
        stun,
        snare
	}
	private states current_state = states.idle;
	private bool is_paused = false;
    private bool can_update
    {
        get
        {
            return !is_paused;
        }
    }

	// timer
	public float rest_time = 1.0f;
	public float move_time = 3.0f;
	private float state_timer = 0.0f;

	// search
	public float search_range = 5.0f;

	// move
	private float max_speed;


	// unit status
    private EnemyUnit status;
	private GameObject target;
	private NavMeshAgent pathfinder;


    // attack
    public float damage = 5;
    public float aura_size = 3;
    private GameObject aura;

	// Use this for initialization
	void Start () {
        anim = GetComponent<EnemyAnimation>();
        status = GetComponent<EnemyUnit>();

        if (status == null) {
            Debug.LogError(gameObject.name + ".MeleeAI : No EnemyUnit script found");
			Application.Quit();
		}

		target = GameObject.Find ("Player");
		if (target == null) {
			Debug.LogError(gameObject.name + ".MeleeAI : No Player object found");
			Application.Quit();
		}
		pathfinder = GetComponent<NavMeshAgent> ();
		if (pathfinder == null) {
			Debug.LogError(gameObject.name + ".MeleeAI : No NavMeshAgent script found");
			Application.Quit();
		}

		pathfinder.enabled = true;

        // create aura
        aura = (GameObject)Instantiate(Resources.Load("Prefabs/EnemyAura"), transform.position, new Quaternion());
        aura.transform.parent = transform;

        var aura_script = aura.GetComponent<EnemyAttackAura>();
        aura_script.damage = GetComponent<Character>().damage;
        aura_script.SetAuraSize(aura_size);
        
        pathfinder.updateRotation = false;
    }
	
	// Update is called once per frame
	void Update () {
		// do nothing if script is paused
        if (!can_update)
			return;

		UpdateState ();

		if (current_state == states.idle) {
            // do idle action
            //애니메이션 스크립트에서 IDLE상태로 바꿈
            anim.applyState(STATE_MONSTER.IDLE);

        } else if (current_state == states.move) {
            // do move action

            //애니메이션 스크립트에서 RUN상태로 바꿈
            anim.applyState(STATE_MONSTER.RUN);
			// Move
			Move ();
		} else if (current_state == states.rest) {
            // do rest action

            anim.applyState(STATE_MONSTER.IDLE);
            // Rest
            Rest ();
		}

	}

	private void UpdateState() {
		state_timer += Time.deltaTime;

		if (current_state == states.idle) {
			if (CanFindTarget ()) {
				ChangeState (states.move);
			}
		} else if (current_state == states.move) {
			if(state_timer > move_time && rest_time != 0) {
				ChangeState(states.rest);
			}
		} else if (current_state == states.rest) {
			if(state_timer > rest_time && move_time != 0){
				ChangeState(states.move);
			}
		}
	}

	private void ChangeState(states next_state) {
		state_timer = 0;

		current_state = next_state;
	}

	private void Move() {
		// move
		pathfinder.speed = status.currentSpeed;
		pathfinder.destination = target.transform.position;
	}


	private void Rest(){
		// move
		pathfinder.speed = 0;
		pathfinder.destination = target.transform.position;
	}

	private bool CanFindTarget() {
		if (Vector3.Magnitude (target.transform.position - transform.position) < search_range) {
			return true;
		}

		return false;
	}


	// stop AI working
    public void pauseAI()
    {
		is_paused = true;
		pathfinder.enabled = false;
	}

	// start AI working
    public void resumeAI()
    {
		is_paused = false;
		pathfinder.enabled = true;
	}

    public void GiveBuff(ENEMY_BUFF buffnum, float rate, float time)
    {

    }

    public void GiveKnockBack(Vector3 direction, float amount, float time)
    {
        gameObject.GetComponent<Rigidbody>().AddForce(direction * amount);
        StartCoroutine(kinematicOnOff(time));
    }

    IEnumerator kinematicOnOff(float time)
    {
        //Kinematic을 켰다 켜 무한히 튕겨나가지 않도록 한다.
        yield return StartCoroutine(DelayedTimer.WaitForCustomDeltaTime(time, GetDeltaTime));
        gameObject.GetComponent<Rigidbody>().isKinematic = true;

        yield return new WaitForFixedUpdate();

        gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }

    float GetDeltaTime()
    {
        if (!can_update)
            return 0;

        return Time.deltaTime;
    }
}
