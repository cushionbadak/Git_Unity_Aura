using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class bosshpshow : MonoBehaviour {

	public GameObject boss;
	public Slider bossslider;
	public GameObject bosscanvas;


	// Use this for initialization
	void Start () {
		bosscanvas.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		var boss_script = boss.GetComponent<Character> ();
		float boss_max_hp = boss_script.maxHP;
		float boss_current_hp = boss_script.currentHP;
		bossslider.maxValue = boss_max_hp;
		bossslider.value = boss_current_hp;

		if (boss_current_hp != boss_max_hp && boss_current_hp != 0) {
			bosscanvas.SetActive (true);
		} else if (boss_current_hp == 0) {
			bosscanvas.SetActive (true);
		}
	

	}
}
