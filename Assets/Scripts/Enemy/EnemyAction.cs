using System;
using UnityEngine;
using System.Collections;

public abstract class EnemyAction : MonoBehaviour{

    /// <summary>
    /// Check this Action is available before acting
    /// </summary>
    /// <returns> available </returns>

    public abstract bool isAvailable();

    /// <summary>
    /// probability cost.
    /// higher occurs more.
    /// </summary>
    /// <returns> cost </returns>
    public abstract int GetProbCost();

    /// <summary>
    /// if this action is end
    /// </summary>
    /// <returns> </returns>
    public abstract bool isEnd();



    /// <summary>
    /// Acting Code fo this action
    /// </summary>
    public abstract void Act();

    public abstract void OnStart();

    public abstract void OnStop();

    public virtual void OnRestart()
    {
        OnStop();
    }
}
