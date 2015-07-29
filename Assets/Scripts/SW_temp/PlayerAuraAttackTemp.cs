using UnityEngine;
using System.Collections;

public class PlayerAuraAttackTemp : Attack {
    GameObject enemy;
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag== "EnemyBody")
        {
            enemy = col.gameObject;
            GameManager.I.attckToEnemy(this, enemy);
        }
       
    }
}
