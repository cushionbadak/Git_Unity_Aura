using UnityEngine;
using System.Collections;

public interface CharacterInterface {
	float maxHP			{ get; set;	}
	float currentHP		{ get; set;	}
	int level			{ get; set;	}
	float originalSpeed { get; set;	}
	float currentSpeed	{ get; set;	}
	void haveKnockBack(Vector3 moveVector);
	void haveStun(float time);
	void haveSnare(float time);
	void Die();
}
