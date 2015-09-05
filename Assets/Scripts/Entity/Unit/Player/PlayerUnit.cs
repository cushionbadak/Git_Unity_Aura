using UnityEngine;
using System.Collections;

public class PlayerUnit : UnitObject {

	[SerializeField]
	SkillData.SkillName[] skill = new SkillData.SkillName[3] {
		SkillData.SkillName.Nothing,
		SkillData.SkillName.Nothing,
		SkillData.SkillName.Nothing
	};

	public int Level = 1;

    public float CharacterCollideSize = 0.5f;

	protected override void OnStateAct() {
		base.OnStateAct();

		Move();
	}


	void Move() {
		float movingDistance = Speed * GetDeltaTime();
		Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 nextDeltaPosition = dir * movingDistance;

        // check with raycast
        var collidermask = LayerMask.GetMask(new string[] { "MapObject" });
        RaycastHit hit;
		bool ishit = Physics.Raycast(transform.position, dir, out hit, movingDistance, collidermask);
        if (ishit)
        {
            if(hit.distance < CharacterCollideSize)
            {
                nextDeltaPosition = new Vector3(0, 0, 0);
            }
            else
            {
                nextDeltaPosition = dir * (hit.distance - CharacterCollideSize);
            }
        }
     
        transform.position += nextDeltaPosition;
	}

	protected override void OnStateDie() {
		base.OnStateDie();
		if (mvStateMachine.IsFirstFrame()) {
			Time.timeScale = 0.1f;
		}
	}

	public virtual void OnLevelUp() {

	}
}
