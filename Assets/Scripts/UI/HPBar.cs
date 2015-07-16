using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HPBar : MonoBehaviour
{
	public float currentHealth;
	public Slider hpSlider;
	bool damaged;
	Character currentHealthPoint = new Character();


	void Awake(){
		hpSlider.maxValue = PlayerLevelData.I.Status [1].maxHP;
		currentHealth = PlayerLevelData.I.Status [1].maxHP;
	}
	
	void Update(){
		if (damaged) {												//damaged must be fixed
			currentHealth = currentHealthPoint.currentHP;
			hpSlider.value = currentHealth;
		}
	}
}