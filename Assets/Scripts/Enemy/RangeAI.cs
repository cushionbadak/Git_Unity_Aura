using UnityEngine;
using System.Collections;

public class RangeAI : MonoBehaviour, EnemyAIInterface
{
    EnemyAnimation anim;
    // states
    enum states
    {
        idle,
        move,
        attack,
        dumbling,
        stunned
    }
    private states current_state = states.idle;
    private states next_state = states.idle;
    private bool is_state_changed = true;
    private bool is_state_changed_on_frame = false;


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
    public bool search_out_on = true;
    public float search_out_range = 20;

    // move
    public float dumbling_speed = 10.0f;
    private float max_speed;

    // attack
    public GameObject attack_object = null;
    public float attack_range = 3;
    public float aura_size = 3;
    private bool need_attack = false;
    private GameObject aura;

    // dumbling
    public float mov_to_dumble_rate = 0.10f; // this calls each tick
    public float atk_to_dumble_rate = 0.10f; // this calls once per end of attack
    public bool can_dumbling = false;
    private Vector3 dumbling_position;

    // unit status
    private EnemyUnit status;
    private GameObject target;
    private NavMeshAgent pathfinder;


    // Use this for initialization
    void Start()
    {
         anim=GetComponent<EnemyAnimation>();
        status = GetComponent<EnemyUnit>();
        if (status == null)
        {
            Debug.LogError(gameObject.name + ".RangeAI : No EnemyUnit script found");
            Application.Quit();
        }

        target = GameObject.Find("Player");
        if (target == null)
        {
            Debug.LogError(gameObject.name + ".RangeAI : No Player object found");
            Application.Quit();
        }
        pathfinder = GetComponent<NavMeshAgent>();
        if (pathfinder == null)
        {
            Debug.LogError(gameObject.name + ".RangeAI : No NavMeshAgent script found");
            Application.Quit();
        }
        pathfinder.enabled = true;

        aura = (GameObject)Instantiate(Resources.Load("Prefabs/EnemyAura"), transform.position, new Quaternion());
        aura.transform.parent = transform;

        var aura_script = aura.GetComponent<EnemyAuraAttack>();
        aura_script.damage = 5;
        aura_script.SetAuraSize(aura_size);
        pathfinder.updateRotation = false;

    }

    // Update is called once per frame
    void Update()
    {
        // do nothing if script is paused
        if (!can_update)
            return;
        is_state_changed_on_frame = false;

        // dumbling cool down always go regardless of any state
        dumbling_timer -= Time.deltaTime;
        if (dumbling_timer < 0)
            dumbling_timer = 0;

        switch (current_state)
        {
            case states.idle:
                StateIdle();
                break;
            case states.move:
                StateMove();
                break;
            case states.attack:
                StateAttack();
                break;
            case states.dumbling:
                StateDumbling();
                break;
            case states.stunned:
                StateStunned();
                break;
        }
    }

    void LateUpdate()
    {

        if (is_state_changed_on_frame)
            is_state_changed = true;
        else
            is_state_changed = false;

        if (is_state_changed)
        {
            current_state = next_state;
        }
    }

    private void ChangeState(states next_state)
    {
        this.next_state = next_state;
        is_state_changed_on_frame = true;
    }

    private void StateIdle()
    {
        if (is_state_changed)
        {
            pathfinder.enabled = false;
        }

        // do idle animation
        anim.applyState(STATE_MONSTER.IDLE);

        // find target
        if (CanFindTarget())
        {
            if (CanAttackTarget())
            {
                ChangeState(states.attack);
            }
            else
            {
                ChangeState(states.move);
            }
        }
    }

    private void StateMove()
    {
        if (is_state_changed)
        {
            pathfinder.enabled = true;
        }

        Move();

        if (CanAttackTarget())
        {

            need_attack = true;
            ChangeState(states.attack);
        }
        else if (!IsFoundedTargetInSight())
        {
            ChangeState(states.idle);
        }
        else if (CanDumbling() && Random.Range(0.0f, 1.0f) < mov_to_dumble_rate)
        {
            ChangeState(states.dumbling);
        }


    }

    private void StateAttack()
    {
        if (is_state_changed)
        {
            pathfinder.enabled = false;
            timer = attack_postdelay_time + attack_predelay_time;
            need_attack = true;
        }

        timer -= Time.deltaTime;

        anim.applyState(STATE_MONSTER.ATTACK1);
        
        if(need_attack && timer < attack_postdelay_time)
            Attack();



        // state change
        if (timer < 0)
        {
            if (CanDumbling() && Random.Range(0.0f, 1.0f) < atk_to_dumble_rate)
            {
                ChangeState(states.dumbling);
            }
            else if (!IsFoundedTargetInSight())
            {
                ChangeState(states.idle);
            }
            else if (CanAttackTarget())
            {
                ChangeState(states.attack);
            }
            else
            {
                ChangeState(states.move);
            }
        }
    }

    private void StateDumbling()
    {
        if (is_state_changed)
        {
            Vector3 dumbling_dir = new Vector3(Random.Range(-1.0f, 1.1f), 0, Random.Range(-1.0f, 1.0f));
            dumbling_dir.Normalize();
            dumbling_position = transform.position + dumbling_dir * dumbling_speed * dumbling_time;

            timer = dumbling_time;
            dumbling_timer = dumbling_cooldown;
        }

        // dumbling
        Dumbling();


        // state change
        if (timer < 0)
        {

            if (CanAttackTarget())
            {
                ChangeState(states.attack);
            }
            else if (!IsFoundedTargetInSight())
            {
                ChangeState(states.idle);
            }
            else
            {
                ChangeState(states.move);
            }
        }
    }


    private void StateStunned()
    {
        if (is_state_changed)
        {
            pathfinder.enabled = false;
        }

        if (timer < 0)
        {
            ChangeState(states.idle);
        }
    }

    private void Move()
    {
        max_speed = status.currentSpeed;

        // move
        pathfinder.stoppingDistance = attack_range;
        pathfinder.speed = max_speed;
        pathfinder.destination = target.transform.position;
    }

    private void Attack()
    {
        need_attack = false;
        //Debug.Log(gameObject.name + ".RangeMobAI.Attack() : Create Object '" + attack_object + "'");
		if (attack_object != null) 
		{
			Vector3 diff = target.transform.position - transform.position;
			float angle = Vector3.Angle(new Vector3(0, 0, 1), diff);
			if(diff.y - diff.x > 0)
				angle = 360 - angle;
			Vector3 euler = new Vector3(0, angle, 0);
			GameObject laser=(GameObject)Instantiate(attack_object, transform.position, Quaternion.Euler(euler));
            laser.GetComponent<EnemyLaser>().damage = gameObject.GetComponent<EnemyUnit>().damage;
		}
    }

    private void Dumbling()
    {

        Debug.Log(gameObject.name + ".RangeMobAI.Dumbling() to " + dumbling_position);
        pathfinder.speed = dumbling_speed;
        pathfinder.destination = dumbling_position;
    }

    private bool CanDumbling()
    {
        if (can_dumbling && dumbling_timer <= 0)
            return true;

        return false;
    }

    private bool CanFindTarget()
    {
        if (Vector3.Magnitude(target.transform.position - transform.position) < search_range)
        {
            return true;
        }

        return false;
    }

    private bool CanAttackTarget()
    {
        if (Vector3.Magnitude(target.transform.position - transform.position) < attack_range)
        {
            return true;
        }

        return false;
    }

    private bool IsFoundedTargetInSight()
    {
        if (Vector3.Magnitude(target.transform.position - transform.position) > search_out_range && search_out_on)
        {
            return false;
        }

        return true;
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
        Debug.Log("A");
        switch (buffnum)
        {
            case ENEMY_BUFF.SNARE:
                {
                    StartCoroutine(PauseAI(time));
                    break;
                }
            case ENEMY_BUFF.STUN:
                {
                    StartCoroutine(PauseAura(time));
                    StartCoroutine(PauseAI(time));
                    break;
                }
        }
    }


    IEnumerator PauseAura(float time)
    {
        var aura_script = aura.GetComponent<EnemyAuraAttack>();
        aura_script.SetAuraSize(0);
        yield return new WaitForSeconds(time);

        aura_script.SetAuraSize(aura_size);
    }

    IEnumerator PauseAI(float time)
    {
        // stop
        ChangeState(states.stunned);

        // wait
        yield return new WaitForSeconds(time);

        // set to idle
        ChangeState(states.idle);
    }

    public void GiveKnockBack(Vector3 direction, float amount, float time)
    {

    }
}