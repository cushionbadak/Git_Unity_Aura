using UnityEngine;
using System.Collections;

public class Appear : MonoBehaviour {
    Color c1;
    Color c2;
    float timeSum=0;
	// Use this for initialization
	void Start () {

        c1 = GetComponent<SpriteRenderer>().color;
        c1.a = 0;
        c2 = GetComponent<SpriteRenderer>().color;
    }
	
	// Update is called once per frame
	void Update () {

        timeSum += Time.deltaTime;


        if (timeSum < 0.25f) 
        transform.Translate(Vector3.up * 0.06f);

        GetComponent<SpriteRenderer>().color = Color.Lerp(c1, c2, timeSum);
    }
}
