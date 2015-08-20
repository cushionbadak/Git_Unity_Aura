using UnityEngine;
using System.Collections;

public class EnemyAttackDamageInstant : EnemyAttacks
{
    public float damage_ratio = 1.0f;
    public float fire_time = 0.0f;
    public float fire_radius = 3.0f;
    public float exist_time = 5.0f;

    public EffectManager.Effects fire_effect = EffectManager.Effects.NONE;

    
    private GameObject player = null;
    private bool is_paused = false;
    
    // Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(Attack());
        StartCoroutine(Destroy());
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    public override void SetWithParentDamage(float parent_damage)
    {
        damage = parent_damage * damage_ratio;
    }

    public override void pause()
    {
        is_paused = true;
    }

    public override void resume()
    {
        is_paused = false;
    }

    IEnumerator Attack()
    {
        yield return StartCoroutine(DelayedTimer.WaitForCustomDeltaTime(fire_time, GetDeltaTime));

        EffectManager.I.createEffect(this.gameObject, fire_effect);
        if (Vector3.Distance(player.transform.position, transform.position) < fire_radius)
        {
            GameManager.I.attackToPlayer(this);
        }
    }

    IEnumerator Destroy()
    {
        yield return StartCoroutine(DelayedTimer.WaitForCustomDeltaTime(exist_time, GetDeltaTime));

        Destroy(transform.parent.gameObject);
    }

    float GetDeltaTime()
    {
        if (is_paused)
            return 0;
        return Time.deltaTime;
    }
}
