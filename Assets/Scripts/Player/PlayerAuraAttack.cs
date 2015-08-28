using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAuraAttack : Attack {

    // Variables
    private float time_store = 0.0f;
    public float auraAttackCooldown = 1.0f;  //초당 1회 공격 - 추후 수정 가능성 있음
    private Vector3 originalScale;
    public float critChance = 0.1f; //크리티컬 확률 10%
    public float vampScale = 0.1f; //흡혈량 공격력 * 0.1

    private float t_randThunder;
    private float cd_randThunder = 1.0f;

    public PlayerUnit _p;
    private List<Collider> _e = null;

	// Use this for initialization
	void Start () {
        originalScale = transform.localScale;
        _e = new List<Collider>();
    }

    // Update is called once per frame
    void Update () {
        time_store += Time.deltaTime;
        t_randThunder += Time.deltaTime;
        damage = _p.damage * _p.powerUpPotionScale;

        this.transform.localScale = new Vector3(1, 0.01f, 1) * _p.AuraRange * _p.rangeUpPotionScale;
    }

    void OnTriggerStay(Collider col)
    {
        if(col.tag=="EnemyBody")
        {
            if(time_store >= auraAttackCooldown)
            {
                Attack();
                time_store = 0.0f;
            }
            if (_p.isThunderShoes && t_randThunder >= cd_randThunder)
            {
                randThunderAttack();
                t_randThunder = 0.0f;
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "EnemyBody")
        {
            _e.Add(col);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "EnemyBody")
        {
            _e.Remove(col);
        }
    }

    private void Attack()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, this.transform.localScale.x/2);
        foreach (var col in colls)
        {
            if (col.tag == "EnemyBody")
            {
                giveAttack(col.gameObject);
            }
        }
    }

    private void randThunderAttack()
    {
        /*
        Collider[] colls = Physics.OverlapSphere(transform.position, this.transform.localScale.x / 2);
        
        Collider target = colls[Random.Range(0, colls.Length - 1)];
        
        if (target.gameObject.tag == "EnemyBody")
        {
            float originalDamage = damage;
            damage = damage * 3;
            GameManager.I.attckToEnemy(this, target.gameObject);
            GameManager.I.giveSnareToEnemy(target.gameObject, 1.0f);
            EffectManager.I.createThunderShoesEffect(target.gameObject);
            damage = originalDamage;
        }
        
        */

        Collider target = _e[Random.Range(0, _e.Count)];
        
        float originalDamage = damage;
        damage = damage * 3;
        GameManager.I.attckToEnemy(this, target.gameObject);
        GameManager.I.giveSnareToEnemy(target.gameObject, 1.0f);
        EffectManager.I.createThunderShoesEffect(target.gameObject);
        damage = originalDamage;

    }

    private void giveAttack(GameObject enemy)
    {
        if (_p.isCriticalKnuckle)
        {
            float originDamage = damage;
            if (Random.value <= critChance)
            {
                damage = damage * 2;
            }
            GameManager.I.attckToEnemy(this, enemy);
            if (_p.isDraculaBrooch)
			{
                EffectManager.I.createRedHealEffect(this.gameObject);
                float nextHP = _p.currentHP + damage * vampScale;
                    
                if (nextHP >= _p.maxHP)
                    _p.currentHP = _p.maxHP;
                else
                    _p.currentHP = nextHP;
			}
			if(_p.isStickyBall){
				int rand=Random.Range(0,2);
				if(rand==0)
				{
					GameManager.I.giveSnareToEnemy(enemy, 0.1f);
				}
			}
            damage = originDamage;
        }
        else
        {
            GameManager.I.attckToEnemy(this, enemy);
            if (_p.isDraculaBrooch)
			{
				EffectManager.I.createRedHealEffect(this.gameObject);
                float nextHP = _p.currentHP + damage * vampScale;

                if (nextHP >= _p.maxHP)
                    _p.currentHP = _p.maxHP;
                else
                    _p.currentHP = nextHP;
            }
            if (_p.isStickyBall)
			{
				int rand=Random.Range(0,2);
				if(rand==0)
				{
                GameManager.I.giveSnareToEnemy(enemy, 0.1f);
				}
			}
        }
        
    }

    void pause() { }
    void resume() { }
}
