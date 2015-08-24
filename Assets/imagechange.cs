using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class imagechange : MonoBehaviour {
	public GameObject skillupdate;
	public Sprite knockback;
	public Sprite laser;
	public Sprite shugo;
	PlayerSkills _skillselect = null;


	// Use this for initialization
	void Start () {
		_skillselect = skillupdate.GetComponent<PlayerSkills> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (_skillselect._skill_1 == PlayerSkills.skillSet.Knockback) {
			this.gameObject.GetComponent<Image> ().sprite = knockback;
		}
		if (_skillselect._skill_1 == PlayerSkills.skillSet.Laser) {
			this.gameObject.GetComponent<Image> ().sprite = laser;
		}
		if (_skillselect._skill_1 == PlayerSkills.skillSet.Nothing) {
			this.gameObject.GetComponent<Image> ().sprite = shugo;
		}
	}
}