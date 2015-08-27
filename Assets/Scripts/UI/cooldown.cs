using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cooldown: MonoBehaviour{
	public Image img;
	public UnityEngine.UI.Button btn;
	public float cooltime;
	public bool disableOnStart = true;
	float leftTime;

	public GameObject skillupdate;
	PlayerSkills _skillselect = null;


	void Start(){
		_skillselect = skillupdate.GetComponent<PlayerSkills> ();
		img = gameObject.GetComponent<Image>();
		if(btn == null)
			btn = gameObject.GetComponent<UnityEngine.UI.Button>();
		if(disableOnStart)
			ResetCoolTime();
	}

	void Update(){
		if (_skillselect._skill_1 == PlayerSkills.skillSet.Knockback) {
			cooltime = 2.0f;
		}
		if (_skillselect._skill_1 == PlayerSkills.skillSet.Laser) {
			cooltime = 5.0f;
		}
		if (_skillselect._skill_1 == PlayerSkills.skillSet.Teleport) {
			cooltime = 0.5f;
		}

		if (leftTime > 0) {
			leftTime -= Time.deltaTime;
			if (leftTime < 0) {
				if (btn)
					btn.enabled = true;
			}
			float ratio = 1.0f - (leftTime / cooltime);
			if (img)
				img.fillAmount = ratio;
		}

		if(Input.GetKeyDown(KeyCode.A) && leftTime<0){
			ResetCoolTime();
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
