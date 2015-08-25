using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cooldownS: MonoBehaviour{
	public Image img;
	public UnityEngine.UI.Button btn;
	public float cooltime = 60.0f;
	public bool disableOnStart = true;
	float leftTime = 60.0f;
	
	public GameObject skillbringin;
	PlayerSkills _skillselect;
	
	void Start(){
		_skillselect = skillbringin.GetComponent<PlayerSkills> ();
		
		img = gameObject.GetComponent<Image>();
		if(btn == null)
			btn = gameObject.GetComponent<UnityEngine.UI.Button>();
		if(disableOnStart)
			ResetCoolTime();
	}
	
	void Update(){
		if(_skillselect._skill_2==PlayerSkills.skillSet.TripleShock){
			cooltime = 2.0f;}
		if(_skillselect._skill_2==PlayerSkills.skillSet.WindBitingSnowBall){
			cooltime = 5.0f;}
		if(_skillselect._skill_2==PlayerSkills.skillSet.Teleport){
			cooltime = 0.5f;}
		
		if (Input.GetKeyDown (KeyCode.A)) {
			ResetCoolTime();
		}
		
		if (leftTime > 0) {
			leftTime -= Time.deltaTime;
			if (leftTime < 0) {
				if (btn)
					btn.enabled = true;
			}
			float ratio = 1.0f - (leftTime / cooltime);
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
