using UnityEngine;
using System.Collections;

public class EnemyAttacks : Attack {
    public virtual void SetWithParentDamage(float parent_damage)
    {
        damage = parent_damage;
    }
}
