using UnityEngine;
using System.Collections;

public class RangeAI : MonoBehaviour, EnemyAIInterface
{
	// states
	enum states {
		idle,
		move,
		attack_predelay,
		attack_postdelay,
		dumbling
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
	public float attack_predelay_time = 1.0f;
	public float attack_postdelay_time = 1.0f;
	public float dumbling_time = 0.2f;
	public float dumbling_cooldown = 1.0f;
	private float timer = 0.0f;
	private float dumbling_timer = 0.0f;
	
	// search
	public float search_range = 5;
	
	// move
	public float dumbling_speed = 10.0f;
	private float max_speed;

	// attack
	public string attack_object = "";
	public float attack_range = 3;
    public float aura_size = 3;
	private bool need_attack = false;
    private GameObject aura;

	// dumbling
	public float dumbling_rate = 0.10f;
	public bool can_dumbling = false;
	private Vector3 dumbling_position;

	// unit status
    private EnemyUnit status;
	private GameObject target;
	private NavMeshAgent pathfinder;


	// Use this for initialization
	void Start () {
        status = GetComponent<EnemyUnit>();
		if (status == null) {
            Debug.LogError(gameObject.name + ".RangeAI : No EnemyUnit script found");
			Application.Quit();
		}
		
		target = GameObject.Find ("Player");
		if (target == null) {
            Debug.LogError(gameObject.name + ".RangeAI : No Player object found");
			Application.Quit();
		}
		pathfinder = GetComponent<NavMeshAgent> ();
		if (pathfinder == null) {
            Debug.LogError(gameObject.name + ".RangeAI : No NavMeshAgent script found");
			Application.Quit();
		}
		pathfinder.enabled = true;

        aura = (GameObject)Instantiate(Resources.Load("Prefabs/EnemyAura"), transform.position, new Quaternion());
        aura.transform.parent = transform;
        var aura_script = aura.GetComponent<EnemyAuraAttack>();
        aura_script.damage = 5;
        aura_script.SetAuraSize(aura_size);
	}
	
	// Update is called once per frame
	void Update () {
		// do nothing if script is paused
        if (!can_update)
			return;

		UpdateState ();

        // actions for each state
		if (current_state == states.idle) {
			// do idle action
		} else if (current_state == states.move) {
			// do move action

			// Move
			Move ();
		} else if (current_state == states.dumbling) {
			// do dumbling action

			// Dumbling
			Dumbling ();
		} else if (current_state == states.attack_predelay) {
			// do predelay action
		} else if (current_state == states.attack_postdelay) {
			// do postdelay action
		}

		// Attack
		if (need_attack) {
			Attack();
		}
	}

	private void UpdateState() {
		timer += Time.deltaTime;
		dumbling_timer += Time.deltaTime;

		if (current_state == states.idle) {
			if (CanFindTarget ()) {
				if (CanAttackTarget ()) {
					ChangeState (states.attack_predelay);
				} else {
					ChangeState (states.move);
				}
			}
		} else if (current_state == states.move) {
			if (CanAttackTarget ()) {
				ChangeState (states.attack_predelay);
			} else if (CanDumbling() && Random.Range (0.0f, 1.0f) < dumbling_rate) {
				ChangeState (states.dumbling);
			}
		} else if (current_state == states.attack_predelay) {
			if (timer > attack_predelay_time) {
				ChangeState (states.attack_postdelay);
			}
		} else if (current_state == states.attack_postdelay) {
			if (timer > attack_postdelay_time) {
				if (CanDumbling() && Random.Range (0.0f, 1.0f) < dumbling_rate) {
					ChangeState (states.dumbling);
				} else if (CanAttackTarget ()) {
					ChangeState (states.attack_predelay);
				} else {
					ChangeState (states.move);
				}
			}
		} else if (current_state == states.dumbling) {
			if (timer > dumbling_time) {
				if (CanAttackTarget ()) {
					ChangeState (states.attack_predelay);
				} else {
					ChangeState (states.move);
				}
			}
		}
	}

	private void ChangeState(states next_state) {
		timer = 0;

		if (current_state == states.dumbling) {
			dumbling_timer = 0;
		}

		if (next_state == states.attack_predelay) {
			// make pathfinder not move
			pathfinder.destination = transform.position;

		} else if (next_state == states.attack_postdelay) {
			// make pathfinder not move

			// attack flag
			need_attack = true;
		} else if (next_state == states.dumbling) {
			Vector3 dumbling_dir = new Vector3(Random.Range(-1.0f, 1.1f), 0, Random.Range(-1.0f, 1.0f));
			dumbling_dir.Normalize ();
			dumbling_position = transform.position + dumbling_dir * dumbling_speed * dumbling_time;
		}
		
		current_state = next_state;
	}
	private void Move() {
		max_speed = status.currentSpeed;

		// move
        pathfinder.stoppingDistance = attack_range;
		pathfinder.speed = max_speed;
		pathfinder.destination = target.transform.position;
	}

	private void Attack() {
		need_attack = false;
		Debug.Log (gameObject.name + ".RangeMobAI.Attack() : Create Object '" + attack_object + "'");
	}

	private void Dumbling() {
		
		Debug.Log (gameObject.name + ".RangeMobAI.Dumbling() to " + dumbling_position);
		pathfinder.speed = dumbling_speed;
		pathfinder.destination = dumbling_position;
	}

	private bool CanDumbling() {
		if (can_dumbling && dumbling_timer > dumbling_cooldown)
			return true;

		return false;
	}

	private bool CanFindTarget() {
		if (Vector3.Magnitude (target.transform.position - transform.position) < search_range) {
			return true;
		}

		return false;
	}

	private bool CanAttackTarget() {
		if (Vector3.Magnitude (target.transform.position - transform.position) < attack_range) {
			return true;
		}
					
		return false;
	}

	// stop AI working
    public void PauseEnemy()
    {
		is_paused = true;
		pathfinder.enabled = false;
	}
	
	// start AI working
    public void StartEnemy()
    {
		is_paused = false;
		pathfinder.enabled = true;
	}

    public void GiveBuff(ENEMY_BUFF buffnum, float rate, float time)
    {

    }

    public void GiveKnockBack(Vector3 direction, float amount, float time)
    {

    }
}
