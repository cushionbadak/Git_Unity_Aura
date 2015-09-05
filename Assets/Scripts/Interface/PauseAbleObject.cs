using UnityEngine;
using System.Collections;

public class PauseAbleObject : MonoBehaviour
{
    bool isObjectPaused = false;
    bool isAnimationPaused = false;

    // Use this for initialization
    void Awake()
    {
        OnAwake();
    }

    void Start()
    {
        OnStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPaused())
            return;

        OnUpdate();
    }

    void LateUpdate()
    {
        if (IsPaused())
            return;
        OnLateUpdate();
    }


    public void PauseObject()
    {
        isObjectPaused = true;
        OnPause();
    }

    public void ResumeObject()
    {
        isObjectPaused = false;
        OnResume();
    }

    public void PauseAnimation()
    {
        isAnimationPaused = true;
        OnPauseAnimation();
    }

    public void ResumeAnimation()
    {
        isAnimationPaused = false;
        OnResumeAnimation();
    }

    protected bool IsPaused()
    {
        return isObjectPaused;
    }

    protected float GetDeltaTime()
    {
        if (IsPaused())
            return 0;
        return Time.deltaTime;
    }

    protected virtual void OnAwake()
    {

    }

    protected virtual void OnStart()
    {

    }

    protected virtual void OnUpdate()
    {

    }

    protected virtual void OnLateUpdate()
    {

    }

    protected virtual void OnPause()
    {

    }

    protected virtual void OnResume()
    {

    }

    protected virtual void OnPauseAnimation()
    {

    }

    protected virtual void OnResumeAnimation()
    {

    }
}

