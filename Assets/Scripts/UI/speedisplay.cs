using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class speedisplay : MonoBehaviour {
	
	public GameObject player;
	Player pl;
	// Use this for initialization
	void Start () {
		pl = player.GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		string str = "이동속도:" + pl.currentSpeed.ToString ();
		GetComponent<Text>().text = str;
	}
}
