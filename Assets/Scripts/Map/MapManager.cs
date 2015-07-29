using UnityEngine;
using System.Collections;

public class MapManager : MonoBehaviour {
    public int CurrentChapter;
    public bool[] chap1 = new bool[8];//깨면 true
    public GameObject[] chap1Doors = new GameObject[8];

	// Use this for initialization
	void Start () {
        for(int i=0;i<chap1.Length;i++)
        {
            chap1[i] = false;
        }
	
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void DoorOpen(int i)
    {
        switch(CurrentChapter)
        {
            case 1:
                {
                    chap1Doors[i].GetComponent<ChangeAlpha>().isTrans = true;
                    break;
                }

        }
    }

    public bool[] getRoomStatus()
    {
        switch(CurrentChapter)
        {
            case 1:
                {
                    return chap1;
                }
            default:
                return null;
        }
    }
}
