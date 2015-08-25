using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class bosslv1hpshow : MonoBehaviour {

	public GameObject slideron;
	public GameObject boss;
	public Slider bosshp;


	void Update(){
		var boss_script = boss.GetComponent<Character> ();
		float boss_max_hp = boss_script.maxHP;
		float boss_current_hp = boss_script.currentHP;
		bosshp.maxValue = boss_max_hp;
		bosshp.value = boss_current_hp;
	}


}