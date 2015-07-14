using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ENEMY_BUFF
{
    STUN,
    SNARE
};

public interface EnemyAIInterface
{
    void resumeAI();
    void pauseAI();
    void GiveBuff(ENEMY_BUFF buffnum, float rate, float time);
    void GiveKnockBack(Vector3 direction, float amount, float time);
}

