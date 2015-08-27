using UnityEngine;
using System.Collections;

public class NewPlayerUnit : NewUnitObject {

	[SerializeField]
	NewSkillData.SkillName[] skill = new NewSkillData.SkillName[3] {
		NewSkillData.SkillName.Nothing,
		NewSkillData.SkillName.Nothing,
		NewSkillData.SkillName.Nothing
	};

	public int Level = 1;

	protected override void OnStateAct() {
		base.OnStateAct();

		Move();
	}


	void Move() {
		float movingDistance = Speed * GetDeltaTime();
		Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

		var collidermask = LayerMask.GetMask(new string[] { "MapObject" });
		bool raycasth = Physics.Raycast(transform.position, new Vector3(dir.x, 0, 0), movingDistance, collidermask);
		bool raycastv = Physics.Raycast(transform.position, new Vector3(0, 0, dir.z), movingDistance, collidermask);

		if (raycasth)
			dir.x = 0;
		if (raycastv)
			dir.z = 0;

		dir.Normalize();
		transform.position += dir * movingDistance;
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
