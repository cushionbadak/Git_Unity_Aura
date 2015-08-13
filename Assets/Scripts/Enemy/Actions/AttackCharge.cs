﻿using UnityEngine;
using System.Collections;

public class AttackCharge : EnemyAction
{
    public float attack_range = 5;
    public float stop_distance = 0.5f;
    public float acting_time = 4;
    public float charging_time = 3;
    public int prob_cost = 3;
    public float rush_speed = 10;
    public float cool_down_time = 10;
    public float speed_ratio = 10;


    private float cool_down_timer = 0;
    public Vector3 direction = new Vector3();
    private bool charging = true;
    private bool rushing = false;
    private bool can_update = true;

    private GameObject player = null;
    private Enemy unit = null;
    private float timer = 1;

    // state
    private bool player_found = false;

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

    void Update()
    {
        cool_down_timer -= GetDeltaTime();
        if (cool_down_timer < 0)
            cool_down_timer = 0;
    }

    public override bool isAvailable()
    {
        bool player_inside = Vector3.Distance(player.transform.position, transform.position) < attack_range;
        
        return player_inside;
    }

    public override int GetProbCost()
    {
        return prob_cost;
    }

    

    public override void OnStart()
    {
        timer = charging_time;
        charging = true;
        rushing = false;
        direction = player.transform.position - transform.position;
        direction.y = 0;
        direction = Vector3.Normalize(direction);

        cool_down_timer = cool_down_time;

        player_found = true;
    }

    public override void Act()
    {
        timer -= Time.deltaTime;

        Debug.Log(charging + " " + rushing);
        // act
        if (charging)
        {
            // do nothing

            if (timer <= 0)
            {
                charging = false;
                rushing = true;
                timer = acting_time - charging_time;
            }
        }
        else if (rushing)
        {
            transform.position += direction * Time.deltaTime * rush_speed * speed_ratio;

            if (timer <= 0)
            {
                charging = false;
                rushing = false;
            }
        }

    }

    public override void OnStop()
    {
    }

    public override bool isEnd()
    {
        return !rushing && !charging;
    }

    public override void OnReset()
    {
        base.OnReset();

        player_found = false;
    }

    float GetDeltaTime()
    {
        if (!can_update)
            return 0;

        return Time.deltaTime;
    }
}
