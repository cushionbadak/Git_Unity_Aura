using UnityEngine;
using System.Collections;

public class PlayerTower : Attack {

    private bool on_attack = true;
    private float t_attack = .0f;
    public float cd_attack = 1.0f;

    private float t_alive = .0f;
    private float cd_alive = 60.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // 공격 주기 체크
        if (!on_attack)
        {
            t_attack += Time.deltaTime;
            if (t_attack >= cd_attack)
            {
                on_attack = true;
                t_attack = .0f;
            }
        }

        // 60초 뒤 파괴
        t_alive += Time.deltaTime;
        if (t_alive >= cd_alive)
        {
            Destroy(transform.parent.gameObject);
        }
	}

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "EnemyBody")
        {
            if(on_attack)
            {
                on_attack = false;
                t_attack = .0f;
                Attack();
            }
        }
    }

    void Attack()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, this.transform.localScale.x / 2);
        foreach (var col in colls)
        {
            if (col.tag == "EnemyBody")
            {
                GameManager.I.attckToEnemy(this, col.gameObject);
            }
        }
    }
}
