using UnityEngine;
using System.Collections;

public class AttackAuraChange : EnemyAction 
{


    public float acting_time = 1;
    public int prob_cost = 3;

    public float aura_increase_amount = 0.1f;
    public float aura_increase_max = 10;

    private float acting_timer = 0;
    private NewEnemyUnit unit;

    private bool can_update = true;

	// Use this for initialization
	void Start () {

        unit = GetComponent<NewEnemyUnit>();
        if (unit == null)
        {
            if (Debug.isDebugBuild)
                Debug.Log(gameObject.name + "No EnemyUnit Script found");
            Application.Quit();
        }
	}

    public override bool isAvailable()
    {
        bool is_range_max = unit.AuraRange >= aura_increase_max;

        return !is_range_max;
    }

    public override int GetProbCost()
    {
        return prob_cost;
    }

    public override bool isEnd()
    {
        bool is_timer_end = acting_timer <= 0;
        bool is_increase_end = unit.AuraRange >= aura_increase_max;

        return is_timer_end || is_increase_end;
    }


    public override void Act()
    {
        acting_timer -= GetDeltaTime();

        unit.AuraRange += aura_increase_amount * GetDeltaTime();
        if (unit.AuraRange > aura_increase_max)
            unit.AuraRange = aura_increase_max;
    }
    public override void OnStart()
    {
        acting_timer = acting_time;
    }

    public override void OnStop()
    {
        
    }

    float GetDeltaTime()
    {
        if (!can_update)
            return 0;

        return Time.deltaTime;
    }
}
