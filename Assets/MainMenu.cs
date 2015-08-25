using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void next(int index)
	{
		switch (index) {
		case 0:
			Application.LoadLevel(1);
			break;

		case 3:
			Application.Quit();
			break;
		}
	}
}
