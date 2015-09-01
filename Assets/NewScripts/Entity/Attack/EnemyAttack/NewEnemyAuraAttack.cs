using UnityEngine;
using System.Collections;

public class NewEnemyAuraAttack : NewAttackObject {
	// status
	public GameObject ownerbody = null;
	public float auraHeight = 0.1f;
	private NewUnitObject unit = null;
	
	// attack cooldown
	public float attackTime = 1.0f;
	private float attackTimer = 0;
	private bool isAttackFrame = false;


	// Use this for initialization
	void Start () {
		// find owner
		if (ownerbody == null) {
			Debug.LogError(name + " : No Owner is Set for aura");
			Application.Quit();
		}

		unit = ownerbody.GetComponent<NewUnitObject>();
		if (unit == null) {
			Debug.LogError(name + " : Wronge object is set to ownerbody");
			Application.Quit();
		}
		SetAttackDamage(unit.Damage);
		SetAttackSource(unit);
	}
	
	// Update is called once per frame
	void Update () {
		float auraRange = unit.AuraRange;
		// set position / scale
		transform.position = unit.transform.position;
		transform.localScale = new Vector3(auraRange, auraHeight, auraRange);


		// attack
		attackTimer -= GetDeltaTime();
		if (attackTimer <= 0) {
			isAttackFrame = true;
			attackTimer = attackTime;
		}
	}

	void OnTriggerStay(Collider col) {
		if (col.tag == "PlayerBody") {
			if (isAttackFrame) {
				AttackToPlayer();
				isAttackFrame = false;
			}
		}
	}


	void AttackToPlayer() {
		SetAttackDamage(unit.Damage);

		// set attack parameter
		AttackParameters attack = new AttackParameters();
		attack.damage = attackDamage;
		
		var player = NewGameManager.Inst.FindPlayer();
		NewGameManager.Inst.GiveAttackTo(player, attackSource, attack);
	}
}
