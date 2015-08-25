using UnityEngine;
using System.Collections;

public class MoveSprite : MonoBehaviour {
	float timeSum=0;
	public float time;
	public float speed;
	public bool isRight;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timeSum += Time.deltaTime;
		if (timeSum < time) {
			if(isRight)
			transform.Translate (speed*Time.deltaTime, 0, 0);
			else
				transform.Translate(-speed*Time.deltaTime,0,0);
		}
	}
}
