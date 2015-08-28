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
    public GameObject pObject_laser;    //레이저 오브젝트 할당

    // Skill - Teleport
    private bool on_teleport = true;
    private float t_teleport = .0f;
    public float cd_teleport = 0.5f;
    private Vector3 tp_movingVec;
    private float v, h;
    private float tp_movingDist = 4.0f;  //텔레포트 이동거리

    // Skill - SpinningCross
    private bool on_cross = true;
    private float t_cross = .0f;
    public float cd_cross = 20.0f;
    public GameObject pAObject_cross; //십자가 오브젝트 할당

    private bool on_activeCross = false;
    private float t_activeCross = .0f;
    private float cd_activeCross = 10.0f;   //스킬 지속시간

    // Skill - Install Tower
    private bool on_tower = true;
    private float t_tower = .0f;
    public float cd_tower = 120.0f;
    public float tower_damage_scale = 1.0f; //타워 데미지 배율 (본체기준)
    public GameObject pObject_tower; //타워 오브젝트 할당

    // Skill - Big & Beautiful - 더미스킬
    private bool on_big = true;
    private float t_big = .0f;
    public float cd_big = 10.0f;

    private bool on_d_big = false;
    private float t_d_big = .0f;
    public float cd_d_big = 10.0f;

    public enum skillSet {
        Nothing,
        Knockback,
        SpinningCross,
        Teleport,
        Laser,
        WindBitingSnowBall,
        TripleShock,
        ShugokuOokiidesu,
        InstallTower
    }

    // 스킬 슬롯 할당

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
        if (!on_knockback)
        {
            t_knockback += Time.deltaTime;
            if (t_knockback >= cd_knockback)
                on_knockback = true;
        }

        if (!on_tripleshock)
        {
            t_tripleshock += Time.deltaTime;
            if (t_tripleshock >= cd_tripleshock)
                on_tripleshock = true;
        }

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

        if (!on_laser)
        {
            t_laser += Time.deltaTime;
            if (t_laser >= cd_laser)
                on_laser = true;
        }

        if (!on_teleport)
        {
            t_teleport += Time.deltaTime;
            if (t_teleport >= cd_teleport)
                on_teleport = true;
        }

        if(!on_cross)
        {
            t_cross += Time.deltaTime;
            if(t_cross >= cd_cross)
            {
                on_cross = true;
                t_cross = .0f;
            }
        }

        if(on_activeCross)  //십자가 켜진상태
        {
            t_activeCross += Time.deltaTime;
            if(t_activeCross >= cd_activeCross)
            {
                on_activeCross = false;
                t_activeCross = .0f;
                pAObject_cross.SetActive(false);    //지속시간 끝나면 꺼버림
            }
        }

        if (!on_tower)
        {
            t_tower += Time.deltaTime;
            if (t_tower >= cd_tower)
            {
                on_tower = true;
                t_tower = .0f;
            }
        }

        if (!on_big)
        {
            t_big += Time.deltaTime;
            if (t_big >= cd_big)
            {
                on_big = true;
                t_big = .0f;
            }
        }

        if (on_d_big)
        {
            t_d_big += Time.deltaTime;
            if (t_d_big >= cd_d_big)
            {
                on_d_big = false;
                t_d_big = .0f;
                _p.transform.localScale /= 2;
            }
        }

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
        if (on_knockback)
        {
            on_knockback = false;
            t_knockback = .0f;

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
    }
    void skill_SpinningCross()
    {
        if(on_cross)
        {
            on_cross = false;
            t_cross = .0f;
            on_activeCross = true;
            t_activeCross = .0f;
            pAObject_cross.SetActive(true);
        }
    }
    void skill_Teleport()
    {
        if (on_teleport)
        {
            on_teleport = false;
            t_teleport = .0f;

            float tp_dist = tp_movingDist;
            
            damage = _p.damage;
            tp_movingVec = new Vector3(h, 0, v);
            tp_movingVec.Normalize();

            var collidewall = Physics.RaycastAll(transform.position, tp_movingVec, (tp_movingDist + 0.5f), LayerMask.GetMask(new string[]{"Walls"}));
            if (collidewall.Length != 0)
            {
                foreach (var walls in collidewall)
                {
                    if (walls.distance < tp_dist)
                        tp_dist = walls.distance;
                    Debug.Log(walls.point);

                }
            }

            transform.parent.position = transform.parent.position + tp_movingVec * tp_dist;
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
			EffectManager.I.createEffect(this.gameObject,EffectManager.Effects.TELEPORT);
        }
    }
    void skill_Laser()
    {
        if (on_laser)
        {
            on_laser = false;
            t_laser = .0f;

            float angle = Vector3.Angle(new Vector3(0, 0, 1), frontVec);
            if (frontVec.y - frontVec.x > 0)
                angle = 360 - angle;
            Vector3 euler = new Vector3(0, angle, 0);
            GameObject p_laser = (GameObject)Instantiate(pObject_laser, transform.position + frontVec * 6, Quaternion.Euler(euler));
            p_laser.GetComponent<PlayerLaser>().damage = _p.damage;
        }
    }
    void skill_WindBitingSnowBall() { }
    void skill_TripleShock()
    {
        if (on_tripleshock || on_tsnextlink)
        {
            
            Collider[] targets = Physics.OverlapSphere(this.transform.position, transform.localScale.x * 0.5f);
            steps_tsnextlink++;

            if (steps_tsnextlink == 1 || steps_tsnextlink == 2)
			{
				EffectManager.I.createEffect(this.gameObject, EffectManager.Effects.THREEHIT_SMALL);
                
                t_tsnextlink = .0f;
                
                foreach(Collider col in targets)
                {
                    if (col.tag == "EnemyBody")
                    {
                        damage = _p.damage * 2;
                        GameManager.I.attckToEnemy(this, col.gameObject);
                        EffectManager.I.createEffect(col.gameObject, EffectManager.Effects.THREEHIT_SMALL);
                        damage = _p.damage;
                    }
                }
               
            }
            else if (steps_tsnextlink == 3)
			{
				EffectManager.I.createEffect(this.gameObject, EffectManager.Effects.THREEHIT_LARGE);
                on_tsnextlink = false;
                on_tripleshock = false;
                t_tsnextlink = .0f;
                t_tripleshock = .0f;

                steps_tsnextlink = 0;
                
                foreach (Collider col in targets)
                {
                    if (col.tag == "EnemyBody")
                    {
                        damage = _p.damage * 3;
                        GameManager.I.attckToEnemy(this, col.gameObject);
                        EffectManager.I.createEffect(col.gameObject, EffectManager.Effects.THREEHIT_LARGE);
                        damage = _p.damage;
                    }
                }
            }
        }
    }
    void skill_ShugokuOokiidesu()
    {
        if (on_big)
        {
            on_big = false;
            on_d_big = true;
            _p.transform.localScale *= 2;
        }
    }
    void skill_InstallTower()
    {
        if (on_tower)
        {
            on_tower = false;
            t_tower = .0f;

            Vector3 euler = new Vector3(90, 0, 0);
            GameObject p_tower = (GameObject)Instantiate(pObject_tower, transform.position, Quaternion.Euler(euler));
            p_tower.GetComponentInChildren<PlayerTower>().damage = _p.damage * tower_damage_scale;
        }
    }

    void callSkillFunc(skillSet skill)
    {
        if(skill == skillSet.Knockback)
        {
            skill_Knockback();
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
        else if (skill == skillSet.SpinningCross)
        {
            skill_SpinningCross();
        }
        else if (skill == skillSet.InstallTower)
        {
            skill_InstallTower();
        }
    }

    void pause() { }
    void resume() { }
}
