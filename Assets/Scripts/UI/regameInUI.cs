using UnityEngine;
using System.Collections;

public class regameInUI : MonoBehaviour {
    public GameObject playerBody;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if(Input.GetKeyDown(KeyCode.R))
        {
            if(playerBody!=null)
            {
                playerBody.SendMessage("reGame", SendMessageOptions.DontRequireReceiver);
            }
        }
	}
}
