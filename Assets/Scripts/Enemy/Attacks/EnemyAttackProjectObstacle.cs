using UnityEngine;
using System.Collections;

public class EnemyAttackProjectObstacle : EnemyAttacks
{
	public float damage_ratio = 1.0f;
	public float project_speed = 2;
	public float fire_radius = 3;
	
	public EffectManager.Effects fire_effect = EffectManager.Effects.NONE;
	
	private Vector3 target_position;
	private Vector3 target_direction;
	
	public float collision_radius = 0.3f;

	public float exist_time = 0;

	private bool is_paused = false;
	private bool is_attacking = false;
	private bool is_attacked = false;

	private NavMeshObstacle navmesh = null;
	private Collider collider = null;

	// Use this for initialization
	void Start () {
		var player = GameObject.FindGameObjectWithTag("Player");
		target_position = player.transform.position;
		target_direction = Vector3.Normalize(target_position - transform.position);

		navmesh = GetComponent<NavMeshObstacle>();
		collider = GetComponent<Collider>();
		
		navmesh.enabled = false;
		collider.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!CanUpdate())
			return;
		
		Move();
		
		if (IsAtTarget() && !is_attacked)
			Attack();
		
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
	
	void Attack()
	{
		is_attacked = true;
		is_attacking = true;
		
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
		StartCoroutine(DestroyAttack());
	}
	
	float GetDeltaTime()
	{
		if (is_paused)
			return 0;
		return Time.deltaTime;
	}
	
	IEnumerator DestroyAttack()
	{
		navmesh.enabled = true;
		collider.enabled = true;
		yield return StartCoroutine(DelayedTimer.WaitForCustomDeltaTime (exist_time, GetDeltaTime));

		Destroy(transform.parent.gameObject);
	}

}
