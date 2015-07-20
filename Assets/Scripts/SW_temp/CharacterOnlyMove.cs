using UnityEngine;
using System.Collections;

public class CharacterOnlyMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        float v, h;
        float speed = 10;
        v = Input.GetAxis("Vertical");
        h = Input.GetAxis("Horizontal");
        Vector3 speedVec = new Vector3(h, v, 0) * Time.deltaTime * speed;

        transform.Translate(speedVec);
    }
}
