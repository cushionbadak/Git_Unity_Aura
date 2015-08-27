using UnityEngine;
using System.Collections;

public class BossStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col)
	{
		ScriptsManager.I.GameModeOff ();
		ScriptsManager.I.scriptMove ();
	}
}
