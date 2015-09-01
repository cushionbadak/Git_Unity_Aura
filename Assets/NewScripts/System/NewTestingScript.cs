using UnityEngine;
using System.Collections;

public class NewTestingScript : MonoBehaviour {
	bool isMenuOn = false;

	public GameObject stununit;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.O))
			NewGameManager.Inst.GameStart();
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (isMenuOn) {
				isMenuOn = false;
				NewGameManager.Inst.OnMenuOff();
			}
			else {
				isMenuOn = true;
				NewGameManager.Inst.OnMenuOn();
			}
		}

		if (Input.GetKeyDown(KeyCode.A)) {
			stununit.GetComponent<NewUnitObject>().GiveStun(3);
		}
	}
}
