using UnityEngine;
using System.Collections;

public class AttackObject : PauseAbleObject
{
    public UnitObject attackSource = null;
    public float attackDamage = 0;
    public float attackDamageMultiply = 1.0f;

    public virtual void SetAttackDamage(float baseDamage)
    {
        attackDamage = baseDamage * attackDamageMultiply;
    }

    public virtual void SetAttackSource(UnitObject source)
    {
        attackSource = source;
    }
}