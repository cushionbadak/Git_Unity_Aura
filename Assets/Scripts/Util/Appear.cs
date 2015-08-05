using UnityEngine;
using System.Collections;

public class Appear : MonoBehaviour {
    Color c1;
    Color c2;
    bool disAppear = false;
    float newTimeSum=0;
    float timeSum=0;
	// Use this for initialization
	void Start () {

        c1 = GetComponent<SpriteRenderer>().color;
        c1.a = 0;
        c2 = GetComponent<SpriteRenderer>().color;
        c2.a = 1;
    }
	
	// Update is called once per frame
	void Update () {

        timeSum += Time.deltaTime;


        if (timeSum < 0.25f) 
        transform.Translate(Vector3.up * 0.06f);
        if(timeSum>0.25f&&timeSum<0.3f)
        {

            transform.Translate(Vector3.down * 0.03f);
        }


        if(timeSum<0.5f)
        {

            GetComponent<SpriteRenderer>().color = Color.Lerp(c1, c2, timeSum*3);
        }
        if(timeSum>1.5f)
        {
            newTimeSum += Time.deltaTime;   
            GetComponent<SpriteRenderer>().color = Color.Lerp(c2, c1, newTimeSum*3);
        }

        if(timeSum>5.0f)
        {
            Destroy(this.gameObject);
        }

        
    }
}
