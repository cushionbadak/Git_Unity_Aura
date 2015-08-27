using UnityEngine;
using System.Collections;

public class AttackParameters {
	public float damage = 0;

	public bool isKnockBackNeed = false;
	public Vector3 knockbackVector = new Vector3(0, 0, 0);

	public bool isStunNeed = false;
	public float stunTime = 0;

	public bool isSnareNeed = false;
	public float snareTime = 0;
	public float snareRate = 0;
}
