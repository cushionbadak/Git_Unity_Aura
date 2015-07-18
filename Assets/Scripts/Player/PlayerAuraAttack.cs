using UnityEngine;
using System.Collections;

public class PlayerAuraAttack : Attack {

    // Variables
    private float time_store = 0.0f;
    private float auraAttackCooldown = 1.0f;  //초당 1회 공격 - 추후 수정 가능성 있음
    private bool isPaused = false;
    private bool isAttacking = false;
    
    private PlayerUnit _p;

    
	// Use this for initialization
	void Start () {
        _p = GetComponentInParent<PlayerUnit>();
        damage = PlayerLevelData.I.Status[_p.level].damage;
        speed = _p.currentSpeed;
	}
	
	// Update is called once per frame
	void Update () {
    
    
    }
    
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "EnemyBody")
            giveAttack(col.gameObject);
    }

    private void giveAttack(GameObject enemy)
    {
        if (isAttacking == true)
        {
            time_store += Time.deltaTime;
            if (auraAttackCooldown <= time_store)
            {
                time_store = 0.0f;
                GameManager.I.attckToEnemy(this, enemy);
            }
        }
    }

    void pause() { }
    void resume() { }
}
