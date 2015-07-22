using UnityEngine;
using System.Collections;

public class EnemyLaser : Attack {
    public float predelay_time = 1f;
    public float postdelay_time = 1f;

    private float timer = 0;
    private bool player_inside = false;
    private bool isHit = false;

    private bool isPaused = false;

	// Use this for initialization
	void Start () {
        GetComponentInChildren<ParticleSystem>().startRotation = transform.localEulerAngles.y * Mathf.PI / 180;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (isPaused)
            return;

        timer += Time.deltaTime;
        if (timer > predelay_time &&timer<predelay_time+1.0f&& isHit == false)//1초동안 발사된다고 가정
        {
            
            if (player_inside)
            {
                Attack();

                isHit = true;
            }

        }

        if (timer > predelay_time + postdelay_time)
        {
            Object.Destroy(gameObject, 0.3f);
        }
	}


    void Attack()
    {
        GameManager.I.makeKnockbackEffect_Player();
		GameManager.I.attackToPlayer (this);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "PlayerBody")
        {
            player_inside = true;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "PlayerBody")
        {
            player_inside = false;
        }
    }


    public override void pause()
    {
        isPaused = true;
    }

    public override void resume()
    {
        isPaused = false;
    }
}
