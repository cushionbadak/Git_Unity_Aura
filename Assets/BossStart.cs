using UnityEngine;
using System.Collections;

public class BossStart : MonoBehaviour {

	bool one;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col)
	{
		if (!one) {
			one = true;
			ScriptsManager.I.GameModeOff ();
			ScriptsManager.I.scriptMove ();
		}
	}
}
