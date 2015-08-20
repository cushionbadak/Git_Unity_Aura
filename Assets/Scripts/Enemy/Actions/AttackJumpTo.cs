using UnityEngine;
using System.Collections;

public class AttackJumpTo : EnemyAction
{
    public float cool_down_time = 3;
    public int prob_cost = 3;
    public float attack_range = 10.0f;
    public float acting_time = 3;

    public float jumping_time = 2;
    public float jumping_distance = 7.0f;

    public bool can_stump = false;
    public GameObject stump_object = null;

    private GameObject player = null;
    private Enemy unit = null;
    private bool can_update = true;

    private Vector3 direction = new Vector3();
    private float a = 0;
    private float timer = 0;
    private float cool_down_timer = 0;
    private float acting_timer = 0;

	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("PlayerBody");
        if (player == null)
        {
            Debug.LogError("Error On Finding Player");
            Application.Quit();
        }

        unit = GetComponent<Enemy>();
        if (unit == null)
        {
            Debug.LogError("Error On Finding Internal Enemy Script");
            Application.Quit();
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        cool_down_timer -= GetDeltaTime();
        if (cool_down_timer <= 0)
            cool_down_timer = 0;
	}

    public override int GetProbCost()
    {
        return prob_cost;
    }

    public override bool isAvailable()
    {
        return (cool_down_timer <= 0) && Vector3.Distance(player.transform.position, transform.position) <= attack_range;
    }


    public override void OnStart()
    {
        acting_timer = acting_time;
        timer = jumping_time; 
        direction = player.transform.position - transform.position;
        direction.y = 0;
        direction = Vector3.Normalize(direction);
        a = jumping_distance * 3 / Mathf.Pow(jumping_time, 3f);

        cool_down_timer = cool_down_time;
    }



    public override void Act()
    {
        acting_timer -= Time.deltaTime;
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            transform.position += direction * (Mathf.Pow(jumping_time / 2 - timer, 2) + Mathf.Pow(jumping_time, 2) / 4) * a * Time.deltaTime;
            if (timer <= 0)
            {
                Attack();
            }
        }
        if (acting_timer <= 0)
        {
            acting_timer = 0;
        }
    }

    public override bool isEnd()
    {
        return acting_timer <= 0;
    }

    public override void OnStop()
    {

    }

    public void Attack()
    {
        if (can_stump)
        {
            if (stump_object != null)
            {
                GameObject fired = GameObject.Instantiate(stump_object, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
                var attack_script = fired.GetComponentInChildren<EnemyAttacks>();
                if (attack_script != null)
                    attack_script.SetWithParentDamage(unit.damage);
                else if (Debug.isDebugBuild)
                    Debug.Log(gameObject.name + "No Attack Script Found");
            }
            else
                Debug.LogError(name + " : No Stumping Object");
        }
    }

    float GetDeltaTime()
    {
        if (!can_update)
            return 0;

        return Time.deltaTime;
    }
}
