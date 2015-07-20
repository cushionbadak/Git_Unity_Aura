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

        p = GameObject.FindWithTag("PlayerBody").GetComponent<Player>();
        
        currentHealth = p.maxHP;

    }
	
	void Update(){
        //damaged must be fixed
        hpSlider.maxValue = p.maxHP;
        currentHealth = p.currentHP;
			hpSlider.value = currentHealth;


    }
}