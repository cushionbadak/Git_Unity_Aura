using UnityEngine;
using System.Collections;
using System;

public class DoorCloseControl : MonoBehaviour {
    MapManager mapM;
    public GameObject door;
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
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerExit(Collider col)
    {
        if(mapM.getRoomStatus()[roomNum]==false)
        if(col.tag=="PlayerBody")
        {
            door.GetComponent<ChangeAlpha>().isTrans = false;
        }
    }
}
