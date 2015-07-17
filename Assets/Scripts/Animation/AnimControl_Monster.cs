using UnityEngine;
using System.Collections;

public class AnimControl_Monster : MonoBehaviour {
    string nam;
    GameObject obj;
    bool isFirstFrame = true;
    Vector3 currentPos, nextPos,lookDir;
    int type;
    enum STATE {R_IDLE,L_IDLE,R_RUN,L_RUN,F_ATTACK1, F_ATTACK2, F_ATTACK3, F_IDLE, F_RUN};
    STATE finalSt;
    bool isRight = true;
	// Use this for initialization

    public AnimControl_Monster(string n,int t, GameObject o)
    {
        type = t;
        int r = Random.Range(0, 1);
        if(r==0)
        {
            isRight = true;
        }
        else
        {
            isRight = false;
        }
        nam = n;
        obj= o;
    }
	
    public void UpdateAnim(STATE_MONSTER state)
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

  

    void changeState(STATE_MONSTER state)
    {
        if (type == 1)
        {

            switch (state)
            {
                case STATE_MONSTER.IDLE:
                    {
                        if (isRight)
                            finalSt = STATE.R_IDLE;
                        else
                            finalSt = STATE.L_IDLE;
                        break;
                    }

                case STATE_MONSTER.ATTACK1:
                    {
                        finalSt = STATE.F_ATTACK1;
                        break;
                    }

                case STATE_MONSTER.RUN:
                    {
                        if (lookDir.x > 0)
                        {
                            isRight = true;
                            finalSt = STATE.R_RUN;
                        }
                        if (lookDir.x < 0)
                        {
                            isRight = false;
                            finalSt = STATE.L_RUN;
                        }
                        break;
                    }
            }
        }
        else if(type==2)
        {
            switch (state)
            {
                case STATE_MONSTER.IDLE:
                    {
                        finalSt = STATE.F_IDLE;
                        break;
                    }
                case STATE_MONSTER.ATTACK1:
                    {
                        finalSt = STATE.F_ATTACK1;
                        break;
                    }
                case STATE_MONSTER.RUN:
                    {
                        finalSt = STATE.F_RUN;
                        break;
                    }
            }
        }
    }


    void changeAnim()
    {
        Debug.Log(finalSt);
        obj.GetComponent<Animator>().SetInteger("state",(int)finalSt);
    }


    /*void changeAnim()
   {

       while (st == monsterState.L_IDLE)
       {
           float tSum = 0;
           for (int i = 0; i < Chapter2.GetComponent<Chap2_monster>().super_l_idle.Length; i++)
           {

               tSum = tSum + Time.deltaTime;
               if (tSum > 0.5)
               {
                   obj.GetComponent<SpriteRenderer>().sprite = Chapter2.GetComponent<Chap2_monster>().super_l_idle[i];
                   tSum = 0;
               }
           }
       }
       if (st == monsterState.R_IDLE)
           for (int i = 0; i < Chapter2.GetComponent<Chap2_monster>().super_r_idle.Length; i++)
               obj.GetComponent<SpriteRenderer>().sprite = Chapter2.GetComponent<Chap2_monster>().super_r_idle[i];


       if (st == monsterState.R_RUN)
           for (int i = 0; i < Chapter2.GetComponent<Chap2_monster>().super_R_RUN.Length; i++)
               obj.GetComponent<SpriteRenderer>().sprite = Chapter2.GetComponent<Chap2_monster>().super_R_RUN[i];

       if(st==monsterState.L_RUN)
           for (int i = 0; i < Chapter2.GetComponent<Chap2_monster>().super_L_RUN.Length; i++)
               obj.GetComponent<SpriteRenderer>().sprite = Chapter2.GetComponent<Chap2_monster>().super_L_RUN[i];
   }*/
}
