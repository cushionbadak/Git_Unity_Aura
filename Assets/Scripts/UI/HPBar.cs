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

        p = GameManager.I.findPlayer();

        Debug.Log("초기화시키고 있음");
        currentHealth = p.maxHP;
		hpSlider.maxValue = p.maxHP;

    }
	
	void Update(){											//damaged must be fixed
			currentHealth = p.currentHP;
			hpSlider.value = currentHealth;

	}
}