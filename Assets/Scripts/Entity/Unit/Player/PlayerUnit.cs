using UnityEngine;
using System.Collections;

public class PlayerUnit : UnitObject
{

    [SerializeField]
    public SkillData.SkillName[] skill = new SkillData.SkillName[3] {
        SkillData.SkillName.Nothing,
        SkillData.SkillName.Nothing,
        SkillData.SkillName.Nothing
    };

    public int Level = 1;

    public float CollideRadius = 0.5f;

    public int MoveColliderMask = -1;

    protected override void OnStart()
    {
        base.OnStart();

        if (MoveColliderMask == -1)
            MoveColliderMask = LayerMask.GetMask(new string[] { "Walls" });
    }

    protected override void OnStateAct()
    {
        base.OnStateAct();

        Move();
    }


    void Move()
    {
        float movingDistance = Speed * GetDeltaTime();
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        dir.Normalize();

        // check with raycast
        RaycastHit hit;
        bool ishit = Physics.Raycast(transform.position, dir, out hit, movingDistance + CollideRadius, MoveColliderMask);
        if (ishit)
        {
            //Debug.Log(hit.distance);
            if (hit.distance < CollideRadius)
            {
                movingDistance = 0;
            }
            else
            {
                movingDistance = hit.distance - CollideRadius;
            }
        }

        transform.position += dir * movingDistance;
    }

    protected override void OnStateDie()
    {
        base.OnStateDie();
        if (mvStateMachine.IsFirstFrame())
        {
            Time.timeScale = 0.1f;
        }
    }

    public virtual void OnLevelUp()
    {

    }
}
