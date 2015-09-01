using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewPlayerAuraAttack : NewAttackObject {
	// status
	public GameObject ownerbody = null;
	public float auraHeight = 0.1f;
	private NewUnitObject unit = null;
	
	// attack cooldown
	public float attackTime = 1.0f;
	private float attackTimer = 0;
	private bool isAttackFrame = false;
	
	public List<NewUnitObject> currentList;
	public List<NewUnitObject> nextList;


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
		SetAttackSource(unit);
		
		currentList = new List<NewUnitObject>();
		nextList = new List<NewUnitObject>();
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
			NewUnitObject script = col.GetComponent<NewUnitObject>();
			if(!nextList.Contains(script))
				nextList.Add(script);
		}
	}

	void LateUpdate() {
		currentList = nextList;
		nextList = new List<NewUnitObject>();
	}


	void AttackToEnemy(NewUnitObject obj) {
		SetAttackDamage(unit.Damage);

		// set attack parameter
		AttackParameters attack = new AttackParameters();
		attack.damage = attackDamage;
		
		NewGameManager.Inst.GiveAttackTo(obj, unit, attack);
	}
}
