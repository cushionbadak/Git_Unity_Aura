using UnityEngine;
using System.Collections;
using System;

public class CountChild : MonoBehaviour {
    public GameObject mapM;
    JustForCount[] ChildTs;
    public int roomNum;
	public bool playerIsIn=false;
	public bool isCleared=false;
	public bool isEmptyRoom=false;
    float Timesum;
    // Use this for initialization
    void Start () {
		roomNum = Convert.ToInt16(name);
	    if((bool)mapM.GetComponent<MapManager>().getRoomStatus()[roomNum]==true)
        {
            Transform[] trChild = GetComponentsInChildren<Transform>();
            foreach(Transform tr in trChild)
            {
                tr.gameObject.SetActive(false);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        Timesum += Time.deltaTime;
        if (Timesum > 1)
        {
			if(playerIsIn)
			{
            Timesum = 0;
            ChildTs = gameObject.GetComponentsInChildren<JustForCount>();
            if (ChildTs.Length <= 0&&!isCleared)
            {
					if(!isEmptyRoom)
						playerLevelUp();
					isCleared=true;
                mapM.GetComponent<MapManager>().getRoomStatus()[roomNum] = true;
                mapM.GetComponent<MapManager>().DoorOpen(roomNum);
            }
			}
        }
    }

	void playerLevelUp()
	{
		GameManager.I.LevelUp ();
	}
}
