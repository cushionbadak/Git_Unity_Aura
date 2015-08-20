using UnityEngine;
using System.Collections;

public class TraceDumbling : EnemyAction
{
    public float cool_down_time = 5;
    public float search_range = 5;
    public float acting_time = 0.5f;
    public int prob_cost = 3;
    public float dumbling_speed = 19;

    private GameObject player = null;
    private Enemy unit = null;
    private float timer = 1;
    private float cool_down_timer = 0;
    private bool is_paused = false;

    private Vector3 direction = new Vector3();
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
	}

    public override bool isAvailable()
    {
        bool is_in_range = Vector3.Distance(player.transform.position, transform.position) < search_range;
        bool is_cool_down_end = cool_down_timer <= 0;

        return is_in_range && is_cool_down_end;
    }

    public override int GetProbCost()
    {
        return prob_cost;
    }

    public override bool isEnd()
    {
        return timer < 0;
    }

    public override void OnStart()
    {
        timer = acting_time;
        cool_down_timer = cool_down_time;

        direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        direction.Normalize();
    }

    public override void Act()
    {
        timer -= Time.deltaTime;

        transform.position += direction * dumbling_speed * Time.deltaTime;
    }

    public override void OnStop()
    {

    }

    float GetDeltaTime()
    {
        if (is_paused)
            return 0;
        return Time.deltaTime;
    }
}
