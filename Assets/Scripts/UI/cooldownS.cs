using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cooldownS: MonoBehaviour{
	public Image img;
	public UnityEngine.UI.Button btn;
	public float cooltime;
	public bool disableOnStart = true;
	float leftTimeS = 0;
	
	public GameObject skillupdate;
	PlayerSkills _skillselect = null;
	
	
	void Start(){
		_skillselect = skillupdate.GetComponent<PlayerSkills> ();
		img = gameObject.GetComponent<Image>();
		if(btn == null)
			btn = gameObject.GetComponent<UnityEngine.UI.Button>();
		if (disableOnStart) {
			leftTimeS = 0f;
			img.fillAmount = 1f;
		}
	}
	
	void Update(){
		if (_skillselect._skill_2 == PlayerSkills.skillSet.Knockback) {
			cooltime = 2.0f;
		}
		if (_skillselect._skill_2 == PlayerSkills.skillSet.Laser) {
			cooltime = 5.0f;
		}
		if (_skillselect._skill_2 == PlayerSkills.skillSet.Teleport) {
			cooltime = 0.5f;
		}
		if (_skillselect._skill_2 == PlayerSkills.skillSet.SpinningCross) {
			cooltime = 20.0f;
		}
		if (_skillselect._skill_2 == PlayerSkills.skillSet.ShugokuOokiidesu) {
			cooltime = 5.0f;
		}
		if (_skillselect._skill_2 == PlayerSkills.skillSet.WindBitingSnowBall) {
			cooltime = 0.5f;
		}
		if (_skillselect._skill_2 == PlayerSkills.skillSet.InstallTower) {
			cooltime = 120.0f;
		}
		if (_skillselect._skill_2 == PlayerSkills.skillSet.Nothing) {
			cooltime = 0.1f;
		}
		
		if (leftTimeS > 0) {
			leftTimeS -= Time.deltaTime;
			if (leftTimeS < 0) {
				if (btn)
					btn.enabled = true;
			}
			float ratio = 1.0f - (leftTimeS / cooltime);
			if (img)
				img.fillAmount = ratio;
		}
		
		if(Input.GetKeyDown(KeyCode.S) && leftTimeS<=0){
			ResetCoolTime();
		}
		
	}
	public bool CheckCoolTime(){
		if(leftTimeS>0)
			return false;
		else
			return true;
	}
	
	public void ResetCoolTime(){
		leftTimeS = cooltime;
		if(btn)
			btn.enabled = false;
	}
}