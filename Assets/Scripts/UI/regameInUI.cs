using UnityEngine;
using System.Collections;

public class regameInUI : MonoBehaviour {
    public GameObject playerBody;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if(Input.GetKeyDown(KeyCode.M))
        {
			Time.timeScale=1.0f;
			Application.LoadLevel(0);
        }
	}
}
