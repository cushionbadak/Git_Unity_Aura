using UnityEngine;
using System.Collections;

public class GotoPlayer : MonoBehaviour {
    GameObject player;
    int a = 0;
    float totalTime = 0.0f;
    Vector3 position;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("PlayerBody");
        position = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        totalTime += Time.deltaTime;
        transform.position=Vector3.Lerp(position, player.transform.position, totalTime );
	}
}
