using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAuraAttack : AttackObject {
	// status
	public GameObject ownerbody = null;
	public float auraHeight = 0.1f;
	private UnitObject unit = null;
	
	// attack cooldown
	public float attackTime = 1.0f;
	private float attackTimer = 0;
	private bool isAttackFrame = false;
	
	public List<UnitObject> currentList;
	public List<UnitObject> nextList;


	// Use this for initialization
	void Start () {
		// find owner
		if (ownerbody == null) {
			Debug.LogError(name + " : No Owner is Set for aura");
			Application.Quit();
		}

		unit = ownerbody.GetComponent<UnitObject>();
		if (unit == null) {
			Debug.LogError(name + " : Wronge object is set to ownerbody");
			Application.Quit();
		}
		SetAttackSource(unit);
		
		currentList = new List<UnitObject>();
		nextList = new List<UnitObject>();
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

		if(isAttackFrame) {
			foreach (var enemy in currentList) {
				AttackToEnemy(enemy);
			}
			isAttackFrame = false;
		}
	}

	void OnTriggerStay(Collider col) {
		if(col.tag == "EnemyBody") {
            UnitObject script = col.GetComponent<UnitObject>();
			if(!nextList.Contains(script))
				nextList.Add(script);
		}
	}

	void LateUpdate() {
		currentList = nextList;
		nextList = new List<UnitObject>();
	}


	void AttackToEnemy(UnitObject obj) {
		SetAttackDamage(unit.Damage);

		// set attack parameter
		AttackParameters attack = new AttackParameters();
		attack.damage = attackDamage;
		
		GameManager.Inst.GiveAttackTo(obj, unit, attack);
	}
}
