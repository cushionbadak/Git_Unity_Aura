using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAuraAttack : Attack {

    // Variables
    private float time_store = 0.0f;
    private float auraAttackCooldown = 1.0f;  //초당 1회 공격 - 추후 수정 가능성 있음
    private bool isPaused = false;
    private bool isAttacking = false;
    public List<GameObject> listOfObjects;
    
    public PlayerUnit _p;

    
	// Use this for initialization
	void Start () {
        listOfObjects = new List<GameObject>();
        listOfObjects.AddRange(GameObject.FindGameObjectsWithTag("EnemyBody"));
    }

    // Update is called once per frame
    void Update () {
        time_store += Time.deltaTime;
        damage = PlayerLevelData.I.Status[_p.level].damage;
        if (time_store > 5.0f)
        {
            listOfObjects.Clear();
            listOfObjects.AddRange(GameObject.FindGameObjectsWithTag("EnemyBody"));
        }
        speed = _p.currentSpeed;
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
        GameManager.I.attckToEnemy(this, enemy);
    }

    void pause() { }
    void resume() { }
}
