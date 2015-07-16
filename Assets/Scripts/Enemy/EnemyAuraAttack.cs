using UnityEngine;
using System.Collections;

public class EnemyAuraAttack : Attack
{
    public float attack_cooldown = 1;
    private float attack_timer = 0;
    private bool player_inside = false;

    private bool isPaused = false;
    

    // Use this for initialization
	void Start ()
    {
        var rigidbody = GetComponent<Rigidbody>();
        if (rigidbody == null)
        {
            Debug.LogError(gameObject.name + ".EnemyAuraAttack : No Rigid Body Attatched");
        }
        else
        {
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;
        }


	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isPaused)
            return;

        attack_timer += Time.deltaTime;

        if (attack_timer > attack_cooldown && player_inside)
        {
            attack_timer = 0;
            GiveAttack();
        }
	}

    void GiveAttack()
    {
        Debug.Log(gameObject.name + ".EnemyAuraAttack : Attack damage : " + damage);
    }


    void OnTriggerEnter(Collider col)
	{
        if (col.gameObject.name == "PlayerBody")
            player_inside = true;
    }

    void OnTriggerExit(Collider col)
    {
		if (col.gameObject.name == "PlayerBody")
            player_inside = false;
    }

    public void SetAuraSize(float aura_size)
    {
        transform.localScale = new Vector3(aura_size, 0.1f, aura_size);
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
