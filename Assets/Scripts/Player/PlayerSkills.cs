using UnityEngine;
using System.Collections;

public class PlayerSkills : Attack {
    // 스킬을 어디에 붙여야 할지 모르겠당. 일단 공격이 전부 오라관련이니까 오라에 붙임.
    // Variables
    private float t_knockback = 0.0f;//넉백시계
    bool knockbackon;
    public float cool_knockback;//넉백 쿨타임
    private int s_id = 0;
    private float time_store = 0.0f; //추후 스킬 쿨다운 적용을 위함
    
	// Use this for initialization
	void Start () {
        t_knockback = cool_knockback;
	}
	
	// Update is called once per frame
	void Update () {
        t_knockback += Time.deltaTime;
        if (t_knockback > cool_knockback)
            knockbackon = true;
        if (Input.GetKeyDown(KeyCode.A)) {
            if (knockbackon)
            {
                t_knockback = 0;
                knockbackon = false;
                skill_Knockback();
            }
        }
        else if (Input.GetKeyDown(KeyCode.S)) { }
        else if (Input.GetKeyDown(KeyCode.D)) { }
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "EnemyBody") { }
    }
    
    void skill_Knockback()
    {
        Collider[] targets = Physics.OverlapSphere(this.transform.position, 5.0f);
        
        foreach (Collider col in targets)
        {
            knockbackVector = (col.gameObject.transform.position - this.transform.position).normalized;
            isknockbackVectorNeed = true;
            damage = .0f;
            GameManager.I.attckToEnemy(this, col.gameObject);
        }
        GameManager.I.makeKnockbackEffect();
    }

    void skill_SpinningCross() {}   //추가 오라 객체를 만들어서 거기에 붙여야 할라나?
    void skill_Teleport() {}
    void skill_Laser() { }
    void skill_WindBitingSnowBall() { }
    void skill_TripleShock() { }
    void skill_ShugokuOokiidesu() { }

    void pause() { }
    void resume() { }
}
