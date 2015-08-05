using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAuraAttack : Attack {

    // Variables
    private float time_store = 0.0f;
    private float auraAttackCooldown = 1.0f;  //초당 1회 공격 - 추후 수정 가능성 있음
    private bool isPaused = false;
    private bool isAttacking = false;
    private Vector3 originalScale;
    private float critChance = 0.1f; //크리티컬 확률 10%
    private float vampScale = 0.1f; //흡혈량 공격력 * 0.1
    public List<GameObject> listOfObjects;
    public PlayerUnit _p;


	// Use this for initialization
	void Start () {
        listOfObjects = new List<GameObject>();
        listOfObjects.AddRange(GameObject.FindGameObjectsWithTag("EnemyBody"));

        originalScale = this.transform.localScale;
    }

    // Update is called once per frame
    void Update () {
        time_store += Time.deltaTime;
        damage = _p.damage * _p.powerUpPotionScale;
        if (time_store > 5.0f)
        {
            listOfObjects.Clear();
            listOfObjects.AddRange(GameObject.FindGameObjectsWithTag("EnemyBody"));
        }

        this.transform.localScale = originalScale * _p.rangeUpPotionScale;
    }
    
    void OnTriggerStay(Collider col)
    {
            if (auraAttackCooldown <= time_store)
        {
            
            if (col.gameObject.tag == "EnemyBody")
            {

                Collider[] colls = Physics.OverlapSphere(transform.position, 5.0f);//5.0f -> 오오라의 크기로 바꿔야 함
                foreach (GameObject go in listOfObjects)
                {
                    foreach (Collider coll in colls)
                        if (go.transform == coll.transform)
                        {
                            time_store = 0.0f;
                            giveAttack(coll.gameObject);
                        }
                }
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
