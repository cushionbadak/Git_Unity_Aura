using UnityEngine;
using System.Collections;

public class DoScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerExit()
	{
		ScriptsManager.I.GameModeOff ();
	}
}
