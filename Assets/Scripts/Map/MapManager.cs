using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapManager : MonoBehaviour {
    public int CurrentChapter;
    public List<bool> mapStatus = new List<bool>();//깨면 true
    public GameObject[] chap1Doors = new GameObject[8];

    void Awake()
    {
        if (Game.current == null)
        {
            switch (CurrentChapter)
            {
                case 1:
                    {
                        for (int i = 0; i < 9; i++)
                            mapStatus.Add(false);
                        break;
                    }
            }
        }
        else
        {
            switch (CurrentChapter)
            {
                case 1:
                    {
                        mapStatus = Game.current.roomStatus;
                        break;
                    }
            }
        }
    }

    // Use this for initialization
    void Start() {
        
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

    public List<bool> getRoomStatus()
    {
        switch(CurrentChapter)
        {
            case 1:
                {
                    return mapStatus;
                }
            default:
                return null;
        }
    }
}
