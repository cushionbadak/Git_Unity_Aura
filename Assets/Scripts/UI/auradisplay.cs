using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class auradisplay : MonoBehaviour {
	
	public GameObject player;
	Player pl;
	// Use this for initialization
	void Start () {
		pl = player.GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		string str = "오오라의 크기:" + pl.AuraRange.ToString ();
		GetComponent<Text>().text = str;
	}
}
