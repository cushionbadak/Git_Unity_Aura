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

    public float CollideRadius = 0.5f;
	protected override void OnStateAct() {
		base.OnStateAct();

		Move();
	}


	void Move() {
		float movingDistance = Speed * GetDeltaTime();
		Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        dir.Normalize();
        Vector3 nextDeltaPosition = dir * movingDistance;

        // check with raycast
        var collidermask = LayerMask.GetMask(new string[] { "Walls" });
        RaycastHit hit;
        bool ishit = Physics.Raycast(transform.position, dir, out hit, movingDistance + CollideRadius, collidermask);
        if (ishit)
        {
            //Debug.Log(hit.distance);
            if (hit.distance < CollideRadius)
            {
                nextDeltaPosition = new Vector3(0, 0, 0);
            }
            else
            {
                nextDeltaPosition = dir * (hit.distance - CollideRadius);
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
