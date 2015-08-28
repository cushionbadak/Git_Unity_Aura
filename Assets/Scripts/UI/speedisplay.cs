using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class speedisplay : MonoBehaviour {
	public float sp;
	public GameObject player;
	Player pl;
	// Use this for initialization
	void Start () {
		pl = player.GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		if (pl.rangeUpPotionScale == 0) {
			sp = pl.currentSpeed;
		} else if (pl.rangeUpPotionScale != 0) {
			sp = pl.currentSpeed * pl.speedUpPotionScale;
		}
		string str = "이동속도:" + sp.ToString ();
		GetComponent<Text>().text = str;
	}
}
