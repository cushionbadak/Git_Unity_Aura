using UnityEngine;
using System.Collections;

public class TraceSeeking : EnemyAction
{

    public float search_range = 5;
    public float acting_time = 0.5f;
    public int prob_cost = 3;


    private GameObject player = null;
    private Enemy unit = null;
    private float timer = 1;

	// Use this for initialization
	void Start () {
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



    public override bool isAvailable()
    {

        return Vector3.Distance(player.transform.position, transform.position) < search_range;
    }


    public override int GetProbCost()
    {
        return prob_cost;
    }


    public override bool isEnd()
    {
        return timer < 0;
    }


    public override void Act()
    {
        timer -= Time.deltaTime;
    }

    public override void OnStart()
    {

        timer = acting_time;
    }

    public override void OnStop()
    {

    }
}
