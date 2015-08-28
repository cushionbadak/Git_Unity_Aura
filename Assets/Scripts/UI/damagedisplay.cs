using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class damagedisplay : MonoBehaviour {
	public float dam;
	public GameObject player;
	Player pl;
	// Use this for initialization
	void Start () {
		pl = player.GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		if (pl.rangeUpPotionScale == 0) {
			dam = pl.damage;
		} else if (pl.rangeUpPotionScale != 0) {
			dam = pl.damage * pl.powerUpPotionScale;
		}
		string str = "데미지:" + dam.ToString ();
		GetComponent<Text>().text = str;
	}
}
