using UnityEngine;
using System.Collections;

public class PlayerSkills : Attack {
    // 스킬을 어디에 붙여야 할지 모르겠당. 일단 공격이 전부 오라관련이니까 오라에 붙임.
    // Variables

    // Skill - Knockback
    private float t_knockback = 0.0f; //시간 저장
    private bool on_knockback = true;
    public float cd_knockback = 2.0f; //쿨타임

    // Skill - Triple Shock
    private float t_tripleshock = 0.0f;
    private bool on_tripleshock = true;
    public float cd_tripleshock = 5.0f;
        
    private float t_tsnextlink = 0.0f;
    private bool on_tsnextlink = false;
    public float cd_tsnextlink = 1.0f;

    private int step_tripleshock = 0;

    // Skill - Teleport

    private int s_id = 0;
    private float time_store = 0.0f; //추후 스킬 쿨다운 적용을 위함
    public enum skillSet {
        Nothing,
        Knockback,
        SpinningCross,
        Teleport,
        Laser,
        WindBitingSnowBall,
        TripleShock,
        ShugokuOokiidesu
    }

    public skillSet _skill_1 = skillSet.Knockback;
    public skillSet _skill_2 = skillSet.Nothing;
    public skillSet _skill_3 = skillSet.Nothing;

	// Use this for initialization
	void Start () {
   
	}
	
	// Update is called once per frame
	void Update () {
        
        // 스킬 쿨다운 관리
        t_knockback += Time.deltaTime;
        if (t_knockback > cd_knockback)
            on_knockback = true;

        t_tripleshock += Time.deltaTime;
        if (t_tripleshock > cd_tripleshock)
            on_tripleshock = true;

        if (on_tsnextlink)
        {
            t_tsnextlink += Time.deltaTime;
            if (t_tsnextlink > cd_tsnextlink)
            {
                on_tsnextlink = false;
                on_tripleshock = false;
                t_tripleshock = .0f;
            }
        }



        // 적절한 스킬 호출하기
        if (Input.GetKeyDown(KeyCode.A))
            callSkillFunc(_skill_1);   
        else if (Input.GetKeyDown(KeyCode.S))
            callSkillFunc(_skill_2);
        else if (Input.GetKeyDown(KeyCode.D))
            callSkillFunc(_skill_3);

	}

    void skill_Knockback()
    {
        Collider[] targets = Physics.OverlapSphere(this.transform.position, 5.0f);
        
        foreach (Collider col in targets)
        {
            if (col.tag == "EnemyBody")
            {
                knockbackVector = (col.gameObject.transform.position - this.transform.position).normalized;
                knockbackVector = knockbackVector * knockbackForce;
                isknockbackVectorNeed = true;
                GameManager.I.giveKnockbackToEnemy(knockbackVector, col.gameObject);
            }
        }
        GameManager.I.makeKnockbackEffect();
    }

    void skill_SpinningCross() {}   //추가 오라 객체를 만들어서 거기에 붙여야 할라나?
    void skill_Teleport() {}
    void skill_Laser() { }
    void skill_WindBitingSnowBall() { }
    void skill_TripleShock() { }
    void skill_ShugokuOokiidesu() { }

    void callSkillFunc(skillSet skill)
    {
        if(skill == skillSet.Knockback)
        {
            if(on_knockback)
            {
                t_knockback = .0f;
                on_knockback = false;
                skill_Knockback();
            }
        }
        else if(skill == skillSet.TripleShock)
        {
            if (on_tripleshock)
            {
                on_tsnextlink = true;
            }
        }
        else
        {
        }
    }

    void pause() { }
    void resume() { }
}
