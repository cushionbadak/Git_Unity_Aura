﻿using UnityEngine;
using System.Collections;

public class AnimControl_Monster {
    string nam;
    GameObject obj;
	bool isSame;
    bool isFirstFrame = true;
    Vector3 currentPos, nextPos,lookDir;
    int type;
	enum STATE {R_IDLE,L_IDLE,R_RUN,L_RUN,F_ATTACK1, F_ATTACK2, F_ATTACK3, F_IDLE, F_RUN,B_ATTACK1, B_ATTACK2, B_ATTACK3, B_IDLE, B_RUN, R_ATTACK1, R_ATTACK2,R_ATTACK3,R_ATTACK4,L_ATTACK1,L_ATTACK2,L_ATTACK3,L_ATTACK4};
    STATE finalSt;
    bool isRight = true;
	Player pl;
	STATE prevState;
	// Use this for initialization

    public AnimControl_Monster(string n,int t, GameObject o)
    {
		pl = GameObject.FindWithTag ("PlayerBody").GetComponent<Player> ();
        type = t;
        int r = Random.Range(0, 2);
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
		
		prevState = finalSt;
        if (type == 1) {

			switch (state) {
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
			case STATE_MONSTER.ATTACK2:
				{
					finalSt = STATE.F_ATTACK2;
					break;
				}
			case STATE_MONSTER.ATTACK3:
				{
					finalSt = STATE.F_ATTACK3;
					break;
				}

			case STATE_MONSTER.RUN:
				{
					if (lookDir.x > 0) {
						isRight = true;
						finalSt = STATE.R_RUN;
					} else if (lookDir.x < 0) {
						isRight = false;
						finalSt = STATE.L_RUN;
					} else {

						if (isRight)
							finalSt = STATE.R_RUN;
						else
							finalSt = STATE.L_RUN;
					}
					break;
				}
			}
		} else if (type == 2) {
			switch (state) {
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
			case STATE_MONSTER.ATTACK2:
				{
					finalSt = STATE.F_ATTACK2;
					break;
				}
			case STATE_MONSTER.ATTACK3:
				{
					finalSt = STATE.F_ATTACK3;
					break;
				}
			case STATE_MONSTER.RUN:
				{
					finalSt = STATE.F_RUN;
					break;
				}
			}
		} else if (type == 3) {
			switch (state) {
			case STATE_MONSTER.IDLE:
				{
					if (obj.transform.position.z - pl.gameObject.transform.position.z > 0)
						finalSt = STATE.F_IDLE;
					else
						finalSt = STATE.B_IDLE;
					break;
				}
			case STATE_MONSTER.ATTACK1:
				{
					if (obj.transform.position.z - pl.gameObject.transform.position.z > 0)
						finalSt = STATE.F_ATTACK1;
					else
						finalSt = STATE.B_ATTACK1;
					break;
				}
			case STATE_MONSTER.ATTACK2:
				{
					if (obj.transform.position.z - pl.gameObject.transform.position.z > 0)
						finalSt = STATE.F_ATTACK2;
					else
						finalSt = STATE.B_ATTACK2;
					break;
				}
			case STATE_MONSTER.ATTACK3:
				{
					if (obj.transform.position.z - pl.gameObject.transform.position.z > 0)
						finalSt = STATE.F_ATTACK3;
					else
						finalSt = STATE.B_ATTACK3;
					break;
				}
			case STATE_MONSTER.RUN:
				{
					if (obj.transform.position.z - pl.gameObject.transform.position.z > 0)
						finalSt = STATE.F_RUN;
					else
						finalSt = STATE.B_RUN;
					break;
				}
			}

		} else if (type == 4) {
			if (obj.transform.position.x - pl.gameObject.transform.position.x > 0)
			{
				isRight=true;
			}
			else
				isRight=false;
			/*if (lookDir.x > 0) {
				isRight = true;
			}
			else if(lookDir.x<0)
				isRight=false;
*/

			switch (state) {
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

				
				if (isRight)
				finalSt = STATE.R_ATTACK1;
				else
					finalSt = STATE.L_ATTACK1;
				break;
			}
			case STATE_MONSTER.ATTACK2:
			{
				
				if (isRight)
				finalSt = STATE.R_ATTACK2;
				else
					finalSt = STATE.L_ATTACK2;
				break;
			}
			case STATE_MONSTER.ATTACK3:
			{
				if (isRight)
				finalSt = STATE.R_ATTACK3;
				else
					finalSt = STATE.L_ATTACK3;
				break;
			}case STATE_MONSTER.ATTACK4:
			{
				if (isRight)
				finalSt = STATE.R_ATTACK4;
				else
					finalSt = STATE.L_ATTACK4;
				break;
			}
				
			case STATE_MONSTER.RUN:
			{
				if (lookDir.x > 0) {
					isRight = true;
					finalSt = STATE.R_RUN;
				} else if (lookDir.x < 0) {
					isRight = false;
					finalSt = STATE.L_RUN;
				} else {
					
					if (isRight)
						finalSt = STATE.R_RUN;
					else
						finalSt = STATE.L_RUN;
				}
				break;
			}
			}
		}
    }


    void changeAnim()
    {
		if (prevState == finalSt)
			isSame = true;
		else
			isSame = false;
		if (type == 4) {
			obj.GetComponent<Animator> ().SetBool ("isNotSame", !isSame);
		}
        obj.GetComponent<Animator>().SetInteger("state",(int)finalSt);
		prevState = finalSt;
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
