using UnityEngine;
using System.Collections;

public class ItemUpDown : MonoBehaviour {
    bool isUp = true;
    public float speed = 0.2f;
    public float reverseTime = 0.5f;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        reverseTime = reverseTime + Time.deltaTime;
        if(reverseTime>1)
        {
            reverseTime = 0;
            isUp = !isUp;
        }

            if (isUp)
            {
                transform.Translate(Vector3.up * Time.deltaTime*speed);
            }
            else
            {
                transform.Translate(Vector3.down * Time.deltaTime * speed);
            }
        }
	}

