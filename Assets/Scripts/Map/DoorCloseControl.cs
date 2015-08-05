using UnityEngine;
using System.Collections;

public class DoorCloseControl : MonoBehaviour {
    public GameObject MapManager;
    public GameObject door;
    public int RoomNum;
    // Use this for initialization
    void Start () {
	  
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerExit(Collider col)
    {
        if((bool)MapManager.GetComponent<MapManager>().getRoomStatus()[RoomNum]==false)
        if(col.tag=="PlayerBody")
        {
            door.GetComponent<ChangeAlpha>().isTrans = false;
        }
    }
}
