using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class ChangeAlpha : MonoBehaviour {
    public bool isTrans = false;
    bool complete;
    Color Color1;
    Color Color2;
    float t;
    float timeSumforTrue=0;
    float timeSumforFalse = 0;
    MapManager mapM;
    public int roomNum;
	// Use this for initialization
	void Start () {
		if (transform.parent.gameObject.name == "SpecialDoor") {
		}
		else
			{
		roomNum = Convert.ToInt16(transform.parent.gameObject.name.Split('r')[1]);
			}
		mapM = GameObject.Find ("Managers").GetComponentInChildren<MapManager> ();
        if(mapM.GetComponent<MapManager>().getRoomStatus()[roomNum]==true)
        {
            isTrans = true;
        }
         Color1 = gameObject.GetComponent<SpriteRenderer>().color;
         Color2 = gameObject.GetComponent<SpriteRenderer>().color;
        Color2.a = 0;
    }
	
	// Update is called once per frame
	void Update () {
            if (isTrans)
            {
                timeSumforFalse = 0;
                timeSumforTrue += Time.deltaTime;
                t = timeSumforTrue;
                gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(Color1, Color2, t);
                if (t > 1)
                {
                    gameObject.GetComponent<BoxCollider>().enabled = false;
                }
            }
            else
            {
                gameObject.GetComponent<BoxCollider>().enabled = true;
                timeSumforTrue = 0;
                timeSumforFalse += Time.deltaTime;
                t = timeSumforFalse;
                gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(Color2, Color1, t);
            }
	}
}
