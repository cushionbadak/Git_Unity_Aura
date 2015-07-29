using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HPBar : MonoBehaviour
{
	public Slider hpSlider;
	Player p= null;

	void Start(){
        p = GameObject.FindWithTag("PlayerBody").GetComponent<Player>();
		hpSlider.maxValue = p.maxHP;
        hpSlider.value = p.maxHP;
    }
	
	void Update(){
        hpSlider.maxValue = p.maxHP;
		hpSlider.value = p.currentHP;
    }
}