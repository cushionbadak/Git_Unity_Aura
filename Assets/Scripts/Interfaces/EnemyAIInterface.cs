using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

interface EnemyAIInterface : EnemyScriptInterface
{
    public enum ENEMY_BUFF
    {
        STUN,
        SLOW,
        STUTTERING
    };

    void GiveBuff(EnemyAIInterface.ENEMY_BUFF buffnum, float rate, float time)
    {

    }
}

