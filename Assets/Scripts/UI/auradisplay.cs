using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class auradisplay : MonoBehaviour {
	public float au;
	public GameObject player;
	Player pl;
	// Use this for initialization
	void Start () {
		pl = player.GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		if (pl.rangeUpPotionScale == 0) {
			au = pl.AuraRange;
		} else if (pl.rangeUpPotionScale != 0) {
			au = pl.AuraRange * pl.rangeUpPotionScale;
		}

		string str = "오오라의 크기:" + au.ToString ();
		GetComponent<Text>().text = str;
	}
}
