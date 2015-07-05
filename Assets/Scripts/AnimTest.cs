using UnityEngine;
using System.Collections;

public class AnimTest: MonoBehaviour {
	Vector3 position;
	bool isWalkDown=false;
	public float speed=1;
	int currentState=1;
	// Use this for initialization
	void Start () {
		position = new Vector3 (0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey (KeyCode.DownArrow)) {
			position.y -= speed;
			gameObject.GetComponent<Animator> ().SetBool ("isWalkDown", true);
			
			currentState = 1;
		} else {
			gameObject.GetComponent<Animator> ().SetBool ("isWalkDown", false);
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			position.x -= speed;
			gameObject.GetComponent<Animator> ().SetBool ("isWalkLeft", true);
			currentState=2;
		} else {
			gameObject.GetComponent<Animator> ().SetBool ("isWalkLeft", false);
			
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			position.x += speed;
			gameObject.GetComponent<Animator> ().SetBool ("isWalkRight", true);
			currentState = 3;
		} else {
			gameObject.GetComponent<Animator> ().SetBool ("isWalkRight", false);
		}
		
		if (Input.GetKey (KeyCode.UpArrow)) {
			position.y += speed;
			gameObject.GetComponent<Animator> ().SetBool ("isWalkUp", true);
			currentState = 4;
		} else {
			gameObject.GetComponent<Animator> ().SetBool ("isWalkUp", false);
		}
		
		
		
		gameObject.GetComponent<Animator> ().SetInteger ("currentState", currentState);
		transform.position += position*Time.deltaTime;
		position = new Vector3 (0, 0, 0);
	}
}
