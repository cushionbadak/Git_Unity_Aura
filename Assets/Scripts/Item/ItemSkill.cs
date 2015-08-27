using UnityEngine;
using System.Collections;

public class ItemSkill : MonoBehaviour 
{
	enum skillslot {
		A,
		S,
		D
	};

	PlayerSkills.skillSet skill = PlayerSkills.skillSet.Nothing;
	skillslot slot = skillslot.A;

	PlayerSkills aura;

	// Use this for initialization
	void Start () 
	{
		aura = GameObject.FindGameObjectWithTag("PlayerAura").GetComponent<PlayerSkills>();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}


	void OnTriggerEnter(Collider col) 
	{
		if (col.tag == "PlayerBody")
		{
			switch (slot) 
			{
				case skillslot.A: 
					{
						aura._skill_1 = skill;
						break;
					}
				case skillslot.S: 
					{
						aura._skill_2 = skill;
						break;
					}
				case skillslot.D: 
					{
						aura._skill_3 = skill;
						break;
					}
			}
		}
	}
}
