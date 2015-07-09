using UnityEngine;
using System.Collections;

public class EnemyUnit : MonoBehaviour, EnemyInterface
{
    public float _max_hp = 100;
    public float maxHP
    { 
        get { return _max_hp; }
        set { _max_hp = value; }
    }

    public float _current_hp = 100;
    public float currentHP 
    {
        get { return _current_hp; }
        set
        {
            if (value < 0)
                _current_hp = 0;
            else
                _current_hp = value;
        }
    }

    private int _level = 0;
    public int level
    { 
        get { return 0; }
        set { }
    }

    public float _original_speed = 100;
    public float originalSpeed 
    {
        get { return _original_speed; }
        set { _original_speed = value; }
    }

    public float currentSpeed
    {
        get { return _original_speed; }
        set { }
    }

    EnemyAIInterface enemyAI;

	// Use this for initialization
	void Start ()
    {
        // find AI
        enemyAI = GetComponent<EnemyAIInterface>();
        if (enemyAI == null)
        {
            Debug.Log(gameObject.name + ".EnemyUnit : No EnemyAI Found");
        }

	}
	
	// Update is called once per frame
	void Update ()
    {
	    // do nothing

	}

    public void haveKnockback(Vector3 moveVector)
    {
        if (enemyAI != null)
            enemyAI.GiveKnockBack(Vector3.Normalize(moveVector), Vector3.Magnitude(moveVector), 0.5f);
    }

    public void haveStun(float time)
    {
        if (enemyAI != null)
            enemyAI.GiveBuff(ENEMY_BUFF.STUN, 0, 3);
    }

    public void haveSnare(float time)
    {
        if (enemyAI != null)
            enemyAI.GiveBuff(ENEMY_BUFF.SNARE, 0, 3);
    }

    public void Die()
    {

    }
}
