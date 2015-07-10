using UnityEngine;
using System.Collections;

public class MainScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			TotalManager.I.setCurrentScene("Test_Chapter1");
			Debug.Log (TotalManager.I.getCurrentScene());
		}
	}
}
