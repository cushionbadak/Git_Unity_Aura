using UnityEngine;
using System.Collections;

public class AttackTowardMultiple : EnemyAction
{

    public float attack_range = 10;
    public int prob_cost = 3;
    public float cool_down = 3;
    public float attack_time = 3;
    public float attack_fire_start_time = 1;
    public float attack_fire_delta_time = 0.5f;
    public int attack_fire_count = 3;

    public GameObject fired_object = null;

    private float cool_down_timer = 0;
    private float attack_timer = 0;
    private bool is_fired = false;

    private GameObject player = null;
    private Enemy unit = null;
    private bool can_update = true;

    // Use this for initialization
    void Start()
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
    void Update()
    {
        // set cool_down
        cool_down_timer -= GetDeltaTime();
        if (cool_down_timer < 0)
            cool_down_timer = 0;
    }

    public override bool isAvailable()
    {
        bool cool_down_end = (cool_down_timer <= 0);
        bool is_player_in_range = Vector3.Distance(player.transform.position, transform.position) < attack_range;
        return cool_down_end && is_player_in_range;
    }

    public override int GetProbCost()
    {
        return prob_cost;
    }

    public override bool isEnd()
    {
        return attack_timer <= 0;
    }


    public override void OnStart()
    {
        cool_down_timer = cool_down;

        attack_timer = attack_time;
        is_fired = false;
    }

    public override void Act()
    {
        attack_timer -= Time.deltaTime;
        if (attack_timer <= attack_time - attack_fire_start_time && !is_fired)
        {
            Attack();
            is_fired = true;
        }
    }

    public override void OnStop()
    {
        is_fired = true;
    }

    float GetDeltaTime()
    {
        if (!can_update)
            return 0;

        return Time.deltaTime;
    }

    void Attack()
    {
        if (fired_object == null)
        {
            if (Debug.isDebugBuild)
                Debug.Log(gameObject.name + " : No Object to be fired");
        }
        else
        {
            if (Debug.isDebugBuild)
                Debug.Log(gameObject.name + " : Fired");

            StartCoroutine(AttackTimes(attack_fire_count));
        }
    }

    IEnumerator AttackTimes(int count)
    {
        for (int i = 0; i < count; ++i)
        {
            SpawnAttackInstance();
            yield return StartCoroutine(DelayedTimer.WaitForCustomDeltaTime(attack_fire_delta_time, GetDeltaTime));
        }
    }


    void SpawnAttackInstance()
    {
        // calculate angle
        Vector3 diff = player.transform.position - transform.position;
        float angle = Vector3.Angle(new Vector3(0, 0, 1), diff);
        if (diff.y - diff.x > 0)
            angle = 360 - angle;
        Vector3 euler = new Vector3(0, angle, 0);

        // create instance
        GameObject fired = GameObject.Instantiate(fired_object, transform.position, Quaternion.Euler(euler)) as GameObject;
        var attack_script = fired.GetComponentInChildren<EnemyAttacks>();
        if (attack_script != null)
            attack_script.SetWithParentDamage(unit.damage);
        else if (Debug.isDebugBuild)
            Debug.Log(gameObject.name + "No Attack Script Found");
    }
}

