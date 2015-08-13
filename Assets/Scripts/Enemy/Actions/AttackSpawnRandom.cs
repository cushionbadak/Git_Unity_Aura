using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackSpawnRandom : EnemyAction
{

    public float attack_range = 10;
    public int prob_cost = 3;
    public float cool_down = 3;
    public float attack_time = 3;
    public float attack_spawn_time = 1;
    public int spawn_count = 1;
    public float spawn_distance = 5;
    public float spawn_between_distance = 1;

    [SerializeField]
    public List<GameObject> spawn_object = null;

    private float cool_down_timer = 0;
    private float attack_timer = 0;
    private bool is_fired = false;

    private GameObject player = null;
    private Enemy unit = null;
    private bool can_update = true;

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
        if (attack_timer <= attack_time - attack_spawn_time && !is_fired)
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
        if (spawn_object == null || spawn_object.Count == 0)
        {
            if (Debug.isDebugBuild)
                Debug.Log(gameObject.name + " : No Object to be spawned");
        }
        else
        {
            if (Debug.isDebugBuild)
                Debug.Log(gameObject.name + " : Spawned");

            // choose random object
            int[] spawnlist = new int[spawn_count];
            for(int i = 0; i < spawn_count; ++i)
                spawnlist[i] = Random.Range(0, spawn_object.Count);
            Spawn(spawnlist);
        }
    }

    void Spawn(int[] index)
    {
        // calculate angle
        Vector3 diff = player.transform.position - transform.position;

        // calculate spawn position
        Vector3 spawn_center = transform.position + Vector3.Normalize(diff) * spawn_distance;
        Vector3 spawn_between = Vector3.Normalize(Vector3.Cross(spawn_center, new Vector3(0, 1, 0))) * spawn_between_distance;

        //calculate spawn between
        Vector3 spawn_start = spawn_center - (float)(spawn_count - 1) / 2 * spawn_between;

        for (int i = 0; i < spawn_count; ++i)
        {
            SpawnSingle(index[i], spawn_start + spawn_between * i);
        }
    }

    void SpawnSingle(int spawn_object_index, Vector3 spawn_position)
    {
        // create instance
        GameObject spawned = GameObject.Instantiate(spawn_object[spawn_object_index], spawn_position, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
    }
}
