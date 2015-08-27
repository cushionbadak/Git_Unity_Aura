using UnityEngine;
using System.Collections;

public class EnemyAttackAura : EnemyAttacks
{
    // attack
    public float attack_cooldown = 1;
    private float attack_timer = 0;


    private bool is_paused = false;
    private bool is_stopped = false;

    private Enemy enemy = null;

    // Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!CanUpdate())
            return;

        if(enemy != null)
            transform.localScale = new Vector3(enemy.AuraRange, 0.1f, enemy.AuraRange);

        attack_timer += Time.deltaTime;
	}

    bool CanAttack()
    {
        return attack_timer > attack_cooldown && !is_stopped;
    }
    void Attack()
    {
		GameManager.I.attackToPlayer (this);
    }


	void OnTriggerStay(Collider col)
	{
		if (CanAttack())
		{
			if(col.gameObject.tag == "PlayerBody")
			{
				attack_timer = 0;
				Attack();
			}
		}
	}

    public void SetOwner(Enemy owner)
    {
        transform.parent = owner.transform;
        enemy = owner;
    }

    public void SetAuraSize(float aura_size)
    {
    }

    public void StopAura()
    {
        is_stopped = true;
    }

    public void ResumeAura()
    {
        is_stopped = false;
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
}
