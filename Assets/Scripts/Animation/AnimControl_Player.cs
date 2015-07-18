using UnityEngine;
using System.Collections;

public class AnimControl_Player : MonoBehaviour
{
    GameObject obj;
    bool isFirstFrame = true;
    Vector3 currentPos, nextPos, lookDir;

    enum NEWS { N,E,W,S};
    enum STATE { R_IDLE, L_IDLE, F_IDLE, B_IDLE, R_RUN, L_RUN, F_RUN, B_RUN};
    STATE finalSt;
    NEWS news;
    bool changeDir=false;
    int digonal;
    // Use this for initialization

    public AnimControl_Player(GameObject o)
    {
        news = NEWS.S;
        obj = o;
    }

    public void UpdateAnim(STATE_PLAYER state)
    {
        if (!isFirstFrame)
        {
            nextPos = obj.transform.position;
            lookDir = (nextPos - currentPos);
            lookDir.Normalize();
            currentPos = obj.transform.position;
        }
        else
        {
            isFirstFrame = false;
            currentPos = obj.transform.position;
        }

        changeState(state);
        changeAnim();
    }



    void changeState(STATE_PLAYER state)
    {
            switch (state)
            {
                case STATE_PLAYER.IDLE:
                 {
                    if (news == NEWS.N)
                        finalSt = STATE.B_IDLE;
                    if (news == NEWS.E)
                        finalSt = STATE.R_IDLE;
                    if (news == NEWS.W)
                        finalSt = STATE.L_IDLE;
                    if (news == NEWS.S)
                        finalSt = STATE.F_IDLE;
                   break;
                 }

                case STATE_PLAYER.RUN:
                    {
                        float x = lookDir.x;
                        float z = lookDir.z;
                             float abx=0;
                            float abz=0;
                    if (x < 0)
                        abx = -x;
                    else
                        abx = x;
                    if (z < 0)
                        abz = -z;
                    else
                        abz = z;
                        if (x >0 &&abx-0.01  > abz )
                        {

                        changeDir = true;
                        news = NEWS.E;
                            finalSt = STATE.R_RUN;
                        }
                        else if (z > 0 &&abz-0.01 > abx)
                    {
                        changeDir = true;
                        news = NEWS.N;
                            finalSt = STATE.B_RUN;
                        }
                        else if (x < 0 && abx-0.01> abz)
                    {
                        changeDir = true;
                        news = NEWS.W;
                            finalSt = STATE.L_RUN;
                        }
                        else if (z < 0 && abz-0.01 > abx )
                    {
                        changeDir = true;
                        news = NEWS.S;
                            finalSt = STATE.F_RUN;
                        }
                    
                         break;
                 }
                }
     }  

    void changeAnim()
    {
        obj.GetComponent<Animator>().SetInteger("state", (int)finalSt);
    }
}
/*else
                     {
                         if (changeDir)
                         {
                             if (x > 0 && z > 0)
                             {
                                 if (finalSt == STATE.R_RUN)
                                     finalSt = STATE.B_RUN;
                                 else if (finalSt == STATE.B_RUN)
                                     finalSt = STATE.R_RUN;
                             }
                             if (x< 0 && z> 0)
                             {
                                 if (finalSt == STATE.L_RUN)
                                     finalSt = STATE.B_RUN;
                                 else if (finalSt == STATE.B_RUN)
                                     finalSt = STATE.L_RUN;
                             }
                             if (x< 0 && z< 0)
                             {
                                 if (finalSt == STATE.L_RUN)
                                     finalSt = STATE.F_RUN;
                                 else if (finalSt == STATE.F_RUN)
                                     finalSt = STATE.L_RUN;
                             }
                             if (x > 0 && z< 0)
                             {
                                 if (finalSt == STATE.R_RUN)
                                     finalSt = STATE.F_RUN;
                                 else if (finalSt == STATE.F_RUN)
                                     finalSt = STATE.R_RUN;
                             }
                         }
                         changeDir = false;
                     }*/