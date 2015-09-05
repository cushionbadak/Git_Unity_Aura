using UnityEngine;
using System.Collections;


/// <summary>
/// this would be called each frame to check time
/// </summary>
/// <returns> delta time of current frame </returns>
public delegate float DELTA_TIME_FUNCTION();

public class DelayedTimer
{


    /// <summary>
    /// Pause Coroutine for a custome timer
    /// </summary>
    /// <param name="time"> timer </param>
    /// <param name="delte_timer"></param>
    public static IEnumerator WaitForCustomDeltaTime(float time, DELTA_TIME_FUNCTION delta_timer)
    {
        float time_left = time;

        while (time_left > 0)
        {
            time_left -= delta_timer();
            yield return null;
        }
    }
}