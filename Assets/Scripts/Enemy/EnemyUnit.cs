using UnityEngine;
using System.Collections;

public class EnemyUnit : Enemy
{
    EnemyAIInterface enemyAI;

	// Use this for initialization
	void Start ()
    {
        currentHP = maxHP;
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
		currentSpeed = originalSpeed;
        if(currentHP<=0)
        {
            transform.parent.gameObject.SetActive(false);
            GameManager.I.EXPIncrease(giveEXP,transform.position);
        }
	}

    /// <summary>
    /// Give KnockBack to this unit
    /// knockback time will be 0.5 second
    /// </summary>
    /// <param name="moveVector"> knockbacked direction * knockbacked amount</param>
    public override void giveKnockback(Vector3 moveVector)
    {
        if (enemyAI != null)
            enemyAI.GiveKnockBack(Vector3.Normalize(moveVector), Vector3.Magnitude(moveVector), 0.5f);
    }

    /// <summary>
    /// Give Stun to this unit
    /// </summary>
    /// <param name="time"> stunned time </param>
    public override void giveStun(float time)
    {
        if (enemyAI != null)
            enemyAI.GiveBuff(ENEMY_BUFF.STUN, 0, 3);
    }

    public override void giveSnare(float time)
    {
        if (enemyAI != null)
            enemyAI.GiveBuff(ENEMY_BUFF.SNARE, 0, 3);
    }

    public override void Die()
    {

    }

    public override void pause()
    {
        if (enemyAI != null)
            enemyAI.pauseAI();
    }

    public override void resume()
    {
        if (enemyAI != null)
            enemyAI.resumeAI();
    }
}
