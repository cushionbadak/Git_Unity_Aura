using UnityEngine;
using System.Collections;

public class NewAttackObject : PauseAbleObject {
	public NewUnitObject attackSource = null;
	public float attackDamage = 0;
	public float attackDamageMultiply = 1.0f;

	public virtual void SetAttackDamage(float baseDamage) {
		attackDamage = baseDamage * attackDamageMultiply;
	}

	public virtual void SetAttackSource(NewUnitObject source) {
		attackSource = source;
	}
}
