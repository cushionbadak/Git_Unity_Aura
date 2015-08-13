using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerLaser : Attack {
    //public float preDelay = 1.0f;
    public float postDelay = 1.0f;
    private float time_store = .0f;
    private List<GameObject> target_list = null;


	// Use this for initialization
	void Start () {
        GetComponent<ParticleSystem>().startRotation = transform.localEulerAngles.y * Mathf.PI / 180;
        target_list = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
        time_store += Time.deltaTime;
        if (time_store > postDelay)
        {
            Attack();
            Destroy(this.gameObject, 0.5f);
        }
	}

    void Attack()
    {
        foreach (var target in target_list)
        {
            GameManager.I.attckToEnemy(this, target);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "EnemyBody")
        {
            target_list.Add(col.gameObject);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "EnemyBody")
        {
            target_list.Remove(col.gameObject);
        }
    }
}
