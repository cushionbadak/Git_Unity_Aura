using UnityEngine;
using System.Collections;

public class skillslot : MonoBehaviour {
	public bool keypress = false;
	void Update(){
		if(Input.GetKeyUp(KeyCode.A)){
			keypress = true;;
		}
	}

	void onkeypress(){
		if(keypress){
			Debug.Log ("KeyPressed");
		}
	}
}
