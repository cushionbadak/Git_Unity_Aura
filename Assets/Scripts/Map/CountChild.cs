using UnityEngine;
using System.Collections;

public class CountChild : MonoBehaviour {
    public GameObject mapM;
    JustForCount[] ChildTs;
    public int roomNum;
    float Timesum;
    // Use this for initialization
    void Start () {
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
            Timesum = 0;
            ChildTs = gameObject.GetComponentsInChildren<JustForCount>();
            if (ChildTs.Length <= 0)
            {
                mapM.GetComponent<MapManager>().getRoomStatus()[roomNum] = true;
                mapM.GetComponent<MapManager>().DoorOpen(roomNum);
            }
        }
    }
}
