using UnityEngine;
using System.Collections;

public class EnemyAuraAttack : MonoBehaviour, AttackInterface {

    private float _damage;
    public float damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    public Vector3 knockbackVector
    {
        get { return new Vector3(0, 0, 0); }
        set { }
    }

    public float stunTime
    {
        get { return 0; }
        set { }
    }

    public float snareTime
    {
        get { return 0; }
        set { }
    }

    public float speed
    {
        get { return 0; }
        set { }
    }

    public float attack_cooldown = 1;
    private float attack_timer = 0;
    private bool player_inside = false;
    

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
        if (col.gameObject.name == "Player")
            player_inside = true;
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "Player")
            player_inside = false;
    }

    public void SetAuraSize(float aura_size)
    {
        transform.localScale = new Vector3(aura_size, 1, aura_size);
    }
}
