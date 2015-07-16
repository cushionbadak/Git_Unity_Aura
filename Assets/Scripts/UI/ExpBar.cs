using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ExpBar : MonoBehaviour
{
	public float currentExp;
	public Slider expSlider;
	Character currentExpPoint = new Character();
	bool killMonster;
	
	
	void Awake(){
		expSlider.maxValue = PlayerLevelData.I.Status [1].needEXP;
		expSlider.value = 0;
	}
	
	void Update(){
		if (killMonster) {												//killMonster must be fixed
			currentExp = currentExp + 5;
			expSlider.value = currentExp;
		}
	}
}