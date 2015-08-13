using UnityEngine;
using System.Collections;

public class EnemyAttackProjectDirection : EnemyAttacks
{
    public float damage_ratio = 1.0f;
    
    public float project_speed = 2;
    public bool speed_accel_apply = false;
    public float speed_accel_amount = 2f;

    public float fire_radius = 3;
    public float collision_radius = 0.3f;

    public bool attack_on_wall = false;
    public bool check_attack_time = false;
    public float attack_time = 10;

    public EffectManager.Effects fire_effect = EffectManager.Effects.NONE;

    private GameObject player;
    private Vector3 target_direction;

    private float attack_timer = 0;
    private bool is_paused = false;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerBody");
        if (player == null)
        {
            if (Debug.isDebugBuild)
            {
                Debug.LogError(gameObject.name + " : No player found");
            }
        }
        float target_angle = transform.parent.localEulerAngles.y / 180 * Mathf.PI;
        target_direction = new Vector3(Mathf.Sin(target_angle), 0, Mathf.Cos(target_angle));

        attack_timer = attack_time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!CanUpdate())
            return;

        Move();
        
        attack_timer -= GetDeltaTime();
        if(attack_timer < 0)
            attack_timer = 0;

        if (IsAtTarget())
        {
            Attack();

            // destroy this attack
            DestroyAttack();
        }
        else if (IsAtWall())
        {
            if (attack_on_wall)
                Attack();

            DestroyAttack();
        }
        
        if (check_attack_time)
        {
            if (attack_timer <= 0)
            {
                Attack();

                DestroyAttack();
            }
        }
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
        if (speed_accel_apply)
            project_speed += speed_accel_amount * GetDeltaTime();
        transform.position += GetDeltaTime() * project_speed * target_direction;
    }

    bool IsAtTarget()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < collision_radius)
            return true;

        return false;
    }

    void Attack()
    {
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

        // give player damage
        if (player_found)
        {
            if (fire_effect != EffectManager.Effects.NONE)
                EffectManager.I.createEffect(this.gameObject, fire_effect);
            GameManager.I.attackToPlayer(this);
        }
    }

    float GetDeltaTime()
    {
        if (is_paused)
            return 0;
        return Time.deltaTime;
    }

    bool IsAtWall()
    {
        var colliders = Physics.OverlapSphere(transform.position, collision_radius);
        foreach (var col in colliders)
        {
            if (col.tag == "MapObject")
            {
                return true;
            }
        }
        return false;
    }


    void DestroyAttack()
    {
        Destroy(transform.parent.gameObject);
    }
}