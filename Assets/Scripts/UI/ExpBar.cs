using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ExpBar : MonoBehaviour
{
	public float currentExp;
	public Slider expSlider;
	bool killMonster;
	Player p = null;
	
	
	void Start(){
		p = GameObject.FindWithTag ("PlayerBody");
		expSlider.maxValue = p.EXP;
		expSlider.value = 0;
	}
	
	void Update(){
		if (killMonster) {												//killMonster must be fixed
			currentExp = currentExp + 5;
			expSlider.value = currentExp;
		}
	}
}