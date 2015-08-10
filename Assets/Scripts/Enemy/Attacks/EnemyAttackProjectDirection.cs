﻿using UnityEngine;
using System.Collections;

public class EnemyAttackProjectDirection : EnemyAttacks
{
    public float damage_ratio = 1.0f;
    public float project_speed = 2;
    public float fire_radius = 3;

    public float collision_radius = 0.3f;

    private GameObject player;
    public Vector3 target_direction;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (!CanUpdate())
            return;

        Move();

        if (IsAtTarget())
            Attack();
        if (IsAtWall())
            DestroyAttack();

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

    bool IsAtWall()
    {
        /*
        var colliders = Physics.OverlapSphere(transform.position, collision_radius);
        foreach (var col in colliders)
        {
            if (col.tag == "MapObject")
            {
                return true;
            }
        }
        return false;
         * */
        return false;
    }


    void DestroyAttack()
    {
        Destroy(transform.parent.gameObject);
    }
}