using UnityEngine;
using System.Collections;

public class PlayerSkill_cross : Attack {

    public PlayerUnit _p;
    public PlayerAuraAttack _pA;
    public float rotateSpeed = 100.0f;
    public float t_crossAttack = 0.0f;
    public float cd_crossAttack = 1.0f;
    private bool canAttack = true;

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 0, -Time.deltaTime * rotateSpeed);

        if(!canAttack)
        {
            t_crossAttack += Time.deltaTime;
            if (t_crossAttack >= cd_crossAttack)
            {
                t_crossAttack = .0f;
                canAttack = true;
            }
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "EnemyBody")
        {
            if(canAttack)
            {
                canAttack = false;
                t_crossAttack = 0.0f;
                Attack(col.gameObject);
            }
        }
    }

    void Attack(GameObject enemy)
    {
        Debug.Log(this.name + "에서 공격 : " + enemy.name);
        if (_p.isCriticalKnuckle)
        {
            float originDamage = _pA.damage;

            if (Random.value <= _pA.critChance)
            {
                damage = originDamage * 2;
            }
            EffectManager.I.createEffect(enemy.gameObject, EffectManager.Effects.CROSSHIT);
            GameManager.I.attckToEnemy(this, enemy);
            if (_p.isDraculaBrooch)
            {
                EffectManager.I.createRedHealEffect(this.gameObject);
                float nextHP = _p.currentHP + damage * _pA.vampScale;

                if (nextHP >= _p.maxHP)
                    _p.currentHP = _p.maxHP;
                else
                    _p.currentHP = nextHP;
            }
            if (_p.isStickyBall)
                GameManager.I.giveSnareToEnemy(enemy, 0.1f);
            damage = originDamage;
        }
        else
        {
            EffectManager.I.createEffect(enemy.gameObject, EffectManager.Effects.CROSSHIT);
            GameManager.I.attckToEnemy(this, enemy);
            if (_p.isDraculaBrooch)
            {
                EffectManager.I.createRedHealEffect(this.gameObject);
                float nextHP = _p.currentHP + damage * _pA.vampScale;

                if (nextHP >= _p.maxHP)
                    _p.currentHP = _p.maxHP;
                else
                    _p.currentHP = nextHP;
            }
            if (_p.isStickyBall)
                GameManager.I.giveSnareToEnemy(enemy, 0.1f);
        }
    }
}
