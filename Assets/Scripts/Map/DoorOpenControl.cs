using UnityEngine;
using System.Collections;
using System;

public class DoorOpenControl : MonoBehaviour {
	
	public int roomNum;
    public GameObject door;
    bool a;
    public bool canOpen;
	// Use this for initialization
	void Start () {
		if (transform.parent.gameObject.name == "SpecialDoor") {
		}
		else
		{
			roomNum = Convert.ToInt16(transform.parent.gameObject.name.Split('r')[1]);
		}
	}
	
	// Update is called once per frame
	void Update () {	
	
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.tag=="PlayerBody"&&canOpen)
        {
		
            door.GetComponent<ChangeAlpha>().isTrans=true;
        }
    }
}
