using UnityEngine;
using System.Collections;

public class PlayerAnimControl : MonoBehaviour {

    // Variables
    private AnimControl_Player animP;
    

	// Use this for initialization
	void Start () {
        animP = new AnimControl_Player(this.gameObject);

	}
	
	// Update is called once per frame
	void Update () {
        bool isMoving;

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if (isMoving)
            animP.UpdateAnim(STATE_PLAYER.RUN);
        else
            animP.UpdateAnim(STATE_PLAYER.IDLE);
	}

}
