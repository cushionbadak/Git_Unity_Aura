using UnityEngine;
using System.Collections;

public class PlayerSkills : Attack {
    // 스킬을 어디에 붙여야 할지 모르겠당. 일단 공격이 전부 오라관련이니까 오라에 붙임.
    // Variables
    private Vector3 frontVec;
    private bool isRight;
    private bool isUp;

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
    private int steps_tsnextlink = 0;

    // Skill - Laser
    private bool on_laser = true;
    private float t_laser = .0f;
    public float cd_laser = 5.0f;

    // Skill - Teleport
    private bool on_teleport = true;
    private float t_teleport = .0f;
    public float cd_teleport = 2.0f;
    public Vector3 tp_movingVec;
    private float v, h;
    private float tp_movingDist = 100.0f;  //텔레포트 이동거리


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

    public PlayerUnit _p;

	// Use this for initialization
	void Start () {
   
	}
	
	// Update is called once per frame
	void Update () {
        
        // 스킬 쿨다운 관리
        t_knockback += Time.deltaTime;
        if (t_knockback >= cd_knockback)
            on_knockback = true;

        t_tripleshock += Time.deltaTime;
        if (t_tripleshock >= cd_tripleshock)
            on_tripleshock = true;

        if (!on_tripleshock)
        {
            t_tripleshock += Time.deltaTime;
            if (t_tripleshock >= cd_tripleshock)
            {
                on_tripleshock = true;
                t_tripleshock = .0f;
                steps_tsnextlink = 0;
            }
        }

        if (on_tsnextlink)
        {
            t_tsnextlink += Time.deltaTime;
            if (t_tsnextlink >= cd_tsnextlink)
            {
                on_tsnextlink = false;
                t_tsnextlink = .0f;
                steps_tsnextlink = 0;
            }
        }

        t_laser += Time.deltaTime;
        if (t_laser >= cd_laser)
            on_laser = true;

        t_teleport += Time.deltaTime;
        if (t_teleport >= cd_teleport)
            on_teleport = true;

        // 적절한 스킬 호출하기
        if (Input.GetKeyDown(KeyCode.A))
            callSkillFunc(_skill_1);   
        else if (Input.GetKeyDown(KeyCode.S))
            callSkillFunc(_skill_2);
        else if (Input.GetKeyDown(KeyCode.D))
            callSkillFunc(_skill_3);

        
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            frontVec = new Vector3(1.0f, 0.0f, 0.0f);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            frontVec = new Vector3(-1.0f, 0.0f, 0.0f);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            frontVec = new Vector3(0.0f, 0.0f, 1.0f);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            frontVec = new Vector3(0.0f, 0.0f, -1.0f);
        }

        v = Input.GetAxis("Vertical");
        h = Input.GetAxis("Horizontal");
	}

    void skill_Knockback()
    {
        Collider[] targets = Physics.OverlapSphere(this.transform.position, this.transform.localScale.x / 2);
       
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
    void skill_Teleport()
    {
        if (on_teleport)
        {
            on_teleport = false;
            t_teleport = .0f;

            Collider[] cols = Physics.OverlapSphere(this.transform.position, this.transform.localScale.x / 2);
            damage = _p.damage * 0.2f;
            foreach (Collider col in cols)
            {
                if (col.tag == "EnemyBody")
                {
                    GameManager.I.giveStunToEnemy(col.gameObject, 1.0f);
                    GameManager.I.attckToEnemy(this, col.gameObject);
                }
            }
            damage = _p.damage;
            tp_movingVec = new Vector3(h, 0, v);
            transform.parent.position = transform.parent.position + tp_movingVec * tp_movingDist;
        }
    }
    void skill_Laser()
    {
        if (on_laser)
        {
            on_laser = false;
            //do something
            //enemy쪽 레이저 발사하는거 이용할수 있을까나?
        }
    }
    void skill_WindBitingSnowBall() { }
    void skill_TripleShock()
    {
        if (on_tripleshock || on_tsnextlink)
        {
            
            Collider[] targets = Physics.OverlapSphere(this.transform.position, _p.AuraRange);
            steps_tsnextlink++;

            if (steps_tsnextlink == 1 || steps_tsnextlink == 2)
            {
                t_tsnextlink = .0f;
                
                foreach(Collider col in targets)
                {
                    if (col.tag == "EnemyBody")
                    {
                        damage = _p.damage * 5;
                        GameManager.I.attckToEnemy(this, col.gameObject);
                        damage = _p.damage;
                    }
                }
            }
            else if (steps_tsnextlink == 3)
            {
                on_tsnextlink = false;
                on_tripleshock = false;
                t_tsnextlink = .0f;
                t_tripleshock = .0f;

                steps_tsnextlink = 0;
                
                foreach (Collider col in targets)
                {
                    if (col.tag == "EnemyBody")
                    {
                        damage = _p.damage * 5;
                        GameManager.I.attckToEnemy(this, col.gameObject);
                        damage = _p.damage;
                    }
                }
            }
        }
    }
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
            skill_TripleShock();
        }
        else if (skill == skillSet.Laser)
        {
            skill_Laser();
        }
        else if (skill == skillSet.Teleport)
        {
            skill_Teleport();
        }
        else
        {
        }
    }

    void pause() { }
    void resume() { }
}
