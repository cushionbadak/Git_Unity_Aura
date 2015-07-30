using UnityEngine;
using System.Collections;

public class ChangeAlpha : MonoBehaviour {
    public bool isTrans = false;
    bool complete;
    Color Color1;
    Color Color2;
    float t;
    float timeSumforTrue=0;
    float timeSumforFalse = 0;
    public GameObject mapM;
    public int roomNum;
	// Use this for initialization
	void Start () {

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
