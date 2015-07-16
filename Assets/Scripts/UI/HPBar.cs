using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HPBar : MonoBehaviour
{
	public float currentHealth;
	public Slider hpSlider;
	bool damaged;
	Player p= null;

	void Start(){
		p = GameObject.FindWithTag ("PlayerBody");
		currentHealth = p.maxHP;
		hpSlider.maxValue = p.maxHP;
	}
	
	void Update(){
		if (damaged) {												//damaged must be fixed
			currentHealth = p.currentHP;
			hpSlider.value = currentHealth;
		}
	}
}