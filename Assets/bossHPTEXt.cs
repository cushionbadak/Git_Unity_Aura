using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class bossHPTEXt : MonoBehaviour {
	Character c;
	public GameObject boss;
	Text t;
	// Use this for initialization
	void Start () {
		t = GetComponent<Text> ();
		c = boss.GetComponent<Character> ();
	}
	
	// Update is called once per frame
	void Update () {
		string str = c.currentHP + "/" + c.maxHP;
		t.text = str;
	}
}
