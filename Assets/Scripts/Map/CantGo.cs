using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CantGo : MonoBehaviour {
     MapManager mapM;
    public int maxNum;
    DoorOpenControl dor;
	bool check=true;
	float timeSum;
	// Use this for initialization
	void Start () {
        dor = GetComponent<DoorOpenControl>();
                mapM = GameObject.Find("Managers").GetComponentInChildren<MapManager>();
	}
	
	// Update is called once per frame
	void Update () {
		timeSum += Time.deltaTime;
		if (timeSum > 1.0f) {
			timeSum=0;
			List<bool> l = mapM.getRoomStatus ();
		
			if (maxNum > 0)
				for (int i=0; i<=maxNum; i++) {
					if (mapM.getRoomStatus () [i] == false)
						check = false;
				}
			if (check) {
				dor.canOpen = true;
			} else {
			}
		}
	}


    void OnTriggerEnter()

    {
		if (!check) {
			SystemMessageManager.I.addMessage ("감히 접근할 수 없습니다.");
		}

	}
       
}
