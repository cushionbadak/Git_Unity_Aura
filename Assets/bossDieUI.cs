using UnityEngine;
using System.Collections;

public class bossDieUI : MonoBehaviour {

	ChangeAlpha_ImageUI a;
	public bool isTrans;
	public GameObject boss;
	Character bossC;
	// Use this for initialization
	void Start () {
		a = GetComponent<ChangeAlpha_ImageUI> ();
		bossC = boss.GetComponent<Character> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isTrans)
			a.alpha0 ();
		else if (isTrans)
			a.alpha1 ();
	}
}
