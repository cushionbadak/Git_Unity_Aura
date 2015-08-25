using UnityEngine;
using System.Collections;

public class bosshpshowcanvas : MonoBehaviour {
	public bool isdamaged;
	public GameObject boss;
	public GameObject bosshpshow;

	// Update is called once per frame

	void Start(){
		bosshpshow.SetActive (false);
	}

	void Update () {
		var boss_script = boss.GetComponent<Character> ();
		float boss_max_hp = boss_script.maxHP;
		float boss_current_hp = boss_script.currentHP;
		if (boss_max_hp != boss_current_hp) {
			isdamaged = true;
		}
		if(isdamaged){
			bosshpshow.SetActive (true);
			boss.SetActive(true);
		}
		if (boss_current_hp == 0.0f) {
			bosshpshow.SetActive (false);
		}
	}
}
