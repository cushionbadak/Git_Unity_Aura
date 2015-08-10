using UnityEngine;
using System.Collections;

public class TraceRunAndRest : EnemyAction 
{

    public float search_range = 5;
    public float stop_distance = 0.5f;
    public float acting_time = 2;
    public float moving_time = 1;
    public int prob_cost = 3;
    public bool find_till_death = true;

    private GameObject player = null;
    private Enemy unit = null;
    private float timer = 1;
    private NavMeshAgent path_finder = null;

    // state
    bool player_found = false;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerBody");
        if (player == null)
        {
            Debug.LogError("Error On Finding Player");
            Application.Quit();
        }

        path_finder = GetComponent<NavMeshAgent>();
        if (path_finder == null)
        {
            Debug.LogError("Error On Finding NavMeshAgent");
            Application.Quit();
        }

        unit = GetComponent<Enemy>();
        if (unit == null)
        {
            Debug.LogError("Error On Finding Internal Enemy Script");
            Application.Quit();
        }

    }

    public override bool isAvailable()
    {
        if (find_till_death && player_found)
            return true;

        bool player_inside = Vector3.Distance(player.transform.position, transform.position) < search_range;

        return player_inside;
    }

    public override int GetProbCost()
    {
        return prob_cost;
    }



    public override void OnStart()
    {
        timer = acting_time;
        path_finder.enabled = true;
        path_finder.stoppingDistance = stop_distance;

        player_found = true;
    }

    public override void Act()
    {
        timer -= Time.deltaTime;
        if (timer > acting_time - moving_time)
        {
            path_finder.speed = unit.currentSpeed;
            path_finder.destination = player.transform.position;
        }
        else
        {
            path_finder.speed = 0;
            path_finder.destination = transform.position;
        }
    }

    public override void OnStop()
    {
        path_finder.enabled = false;

        player_found = false;
    }

    public override void OnRestart()
    {
        path_finder.enabled = true;
    }

    public override bool isEnd()
    {
        return timer < 0;
    }
}
