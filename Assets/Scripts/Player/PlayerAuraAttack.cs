using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAuraAttack : Attack {

    // Variables
    private float time_store = 0.0f;
    public float auraAttackCooldown = 1.0f;  //초당 1회 공격 - 추후 수정 가능성 있음
    private Vector3 originalScale;
    private float critChance = 0.1f; //크리티컬 확률 10%
    private float vampScale = 0.1f; //흡혈량 공격력 * 0.1

    public PlayerUnit _p;


	// Use this for initialization
	void Start () {
        originalScale = this.transform.localScale;
    }

    // Update is called once per frame
    void Update () {
        time_store += Time.deltaTime;
        damage = _p.damage * _p.powerUpPotionScale;
        if (time_store > auraAttackCooldown)
        {
            Attack();
            time_store = 0.0f;
        }

        this.transform.localScale = originalScale * _p.rangeUpPotionScale;
    }


    void Attack()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, _p.AuraRange);
        foreach (var col in colls)
        {
            
            if (col.tag == "EnemyBody")
            {
                giveAttack(col.gameObject);
            }
        }

    }

    private void giveAttack(GameObject enemy)
    {
        if (_p.isCriticalKnuckle == true)
        {
            if (Random.value <= critChance)
            {
                damage = damage * 2;
                GameManager.I.attckToEnemy(this, enemy);
                if (_p.isDraculaBrooch == true)
                    _p.currentHP += damage * vampScale;
                damage = damage / 2;
            }
        }
        else
        {
            GameManager.I.attckToEnemy(this, enemy);
            if (_p.isDraculaBrooch == true)
                _p.currentHP += damage * vampScale;
        }
        
    }

    void pause() { }
    void resume() { }
}
