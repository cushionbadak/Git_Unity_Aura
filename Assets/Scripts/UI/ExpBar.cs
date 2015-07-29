using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ExpBar : MonoBehaviour
{
	public Slider expSlider;
	Player p = null;
	int expneed;

	void Start(){
		p = GameObject.FindWithTag("PlayerBody").GetComponent<Player>();
		expneed = PlayerLevelData.I.Status[p.level +1].needEXP;
	}
	
	void Update(){
		expneed = PlayerLevelData.I.Status [p.level + 1].needEXP;
		expSlider.maxValue = expneed;
		expSlider.value = p.EXP;
	}
}