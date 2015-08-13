using UnityEngine;
using System.Collections;

public class EnemyAttackProjectPosition : EnemyAttacks
{
    public float damage_ratio = 1.0f;
    public float project_speed = 2;
    public float fire_time = 1;
    public float fire_radius = 3;

    public float collision_radius = 0.3f;

    public EffectManager.Effects fire_effect = EffectManager.Effects.NONE;

    private Vector3 target_position;
    private Vector3 target_direction;

    private bool is_paused = false;
    private bool is_attacking = false;
    
	// Use this for initialization
	void Start () {
        var player = GameObject.FindGameObjectWithTag("Player");
        target_position = player.transform.position;
        target_direction = Vector3.Normalize(target_position - transform.position);
	}
	
	// Update is called once per frame
	void Update () {
        if (!CanUpdate())
            return;

        Move();

        if (IsAtTarget())
            StartCoroutine(this.Attack());

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

    bool CanUpdate()
    {
        return !is_paused;
    }

    void Move()
    {
        if (is_attacking)
            return;
        float distance = Vector3.Distance(target_position, transform.position);
        if (distance > Time.deltaTime * project_speed)
            distance = Time.deltaTime * project_speed;
        transform.position += distance * target_direction;
    }

    bool IsAtTarget()
    {
        if (Vector3.Distance(target_position, transform.position) < collision_radius)
            return true;

        return false;
    }

    IEnumerator Attack()
    {
        is_attacking = true;
        yield return StartCoroutine(DelayedTimer.WaitForCustomDeltaTime(fire_time, GetDeltaTime));

        // find player
        bool player_found = false;
        var colliders = Physics.OverlapSphere(transform.position, fire_radius);
        foreach (var col in colliders)
        {
            if (col.tag == "PlayerBody")
            {
                player_found = true;
                break;
            }
        }
        EffectManager.I.createEffect(this.gameObject, fire_effect);

        // give player damage
        if(player_found)
            GameManager.I.attackToPlayer(this);


        // destroy this attack
        DestroyAttack();
    }

    float GetDeltaTime()
    {
        if (is_paused)
            return 0;
        return Time.deltaTime;
    }

    void DestroyAttack()
    {
        Destroy(transform.parent.gameObject);
    }
}
