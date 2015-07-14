using UnityEngine;
using System.Collections;

public class EnemyUnit : Enemy
{
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

    public void pause()
    {
        enemyAI.pauseAI();
    }

    public void resume()
    {
        enemyAI.resumeAI();
    }
}
