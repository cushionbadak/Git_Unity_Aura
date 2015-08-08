using UnityEngine;
using System.Collections;

public class CutSceneAnim_Player : MonoBehaviour {

    private AnimControl_Player animP;
     GameObject plDum;
    private Transform curTr;
    private Vector3 prevPos;
    // Use this for initialization
    void Start () {
        plDum = this.gameObject;
        animP = new AnimControl_Player(this.gameObject);
        curTr = plDum.transform;
        prevPos = curTr.position;
    }
	
	// Update is called once per frame
	void Update () {

        bool isMoving;

        if (curTr.position!=prevPos)
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

        prevPos = curTr.position;
    }
}
