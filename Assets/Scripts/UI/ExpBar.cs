using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ExpBar : MonoBehaviour
{
	public float currentExp;
	public Slider expSlider;
	Player p = null;
	
	
	void Start(){
		p = GameManager.I.findPlayer();
		expSlider.maxValue = p.EXP;
		expSlider.value = 0;
	}
	
	void Update(){											//killMonster must be fixed
			currentExp = currentExp + 5;
			expSlider.value = currentExp;
		
	}
}