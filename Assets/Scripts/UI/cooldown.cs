using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cooldown: MonoBehaviour{
	public Image img;
	public UnityEngine.UI.Button btn;
	public float cooltime = 2.0f;
	public bool disableOnStart = true;
	float leftTime = 2.0f;

	void Start(){
		img = gameObject.GetComponent<Image>();
		if(btn == null)
			btn = gameObject.GetComponent<UnityEngine.UI.Button>();
		if(disableOnStart)
			ResetCoolTime();
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.A)){
			leftTime -= Time.deltaTime;
			if(leftTime<0){
				if (btn)
					btn.enabled = true;
			}
			float ratio = 1.0f - (leftTime/cooltime);
			if(img)
				img.fillAmount = ratio;
		}
	}
	public bool CheckCoolTime(){
		if(leftTime>0)
			return false;
		else
			return true;
	}

	public void ResetCoolTime(){
		leftTime = cooltime;
		if(btn)
			btn.enabled = false;
	}
}
