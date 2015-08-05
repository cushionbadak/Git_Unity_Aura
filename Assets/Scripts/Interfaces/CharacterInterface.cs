using UnityEngine;
using System.Collections;

public interface CharacterInterface {
	float maxHP			{ get; set;	}
	float currentHP		{ get; set;	}
	int level			{ get; set;	}
	float originalSpeed { get; set;	}
	float currentSpeed	{ get; set;	}
	void giveKnockback(Vector3 moveVector);
	void giveStun(float time);
	void giveSnare(float time);
	void Die();
}
