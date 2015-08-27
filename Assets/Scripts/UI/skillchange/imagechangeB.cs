using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class imagechangeB : MonoBehaviour {
	public GameObject skillupdate;
	public Sprite knockback;
	public Sprite laser;
	public Sprite nothing;
	public Sprite teleport;
	public Sprite tripleshock;
	public Sprite towerinstall;
	public Sprite spinningcross;
	public Sprite tower;

	public Sprite shugo;
	PlayerSkills _skillselect = null;

	
	// Use this for initialization
	void Start () {
		_skillselect = skillupdate.GetComponent<PlayerSkills> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (_skillselect._skill_2 == PlayerSkills.skillSet.Knockback) {
			this.gameObject.GetComponent<Image> ().sprite = knockback;
		}
		if (_skillselect._skill_2 == PlayerSkills.skillSet.Laser) {
			this.gameObject.GetComponent<Image> ().sprite = laser;
		}
		if (_skillselect._skill_2 == PlayerSkills.skillSet.Nothing) {
			this.gameObject.GetComponent<Image> ().sprite = nothing;
		}
		if (_skillselect._skill_2 == PlayerSkills.skillSet.Teleport) {
			this.gameObject.GetComponent<Image> ().sprite = teleport;
		}
		if (_skillselect._skill_2 == PlayerSkills.skillSet.TripleShock) {
			this.gameObject.GetComponent<Image> ().sprite = tripleshock;
		}
		if (_skillselect._skill_2 == PlayerSkills.skillSet.InstallTower) {
			this.gameObject.GetComponent<Image> ().sprite = towerinstall;
		}
		if (_skillselect._skill_2 == PlayerSkills.skillSet.SpinningCross) {
			this.gameObject.GetComponent<Image> ().sprite = spinningcross;
		}
		if (_skillselect._skill_2 == PlayerSkills.skillSet.ShugokuOokiidesu) {
			this.gameObject.GetComponent<Image> ().sprite = shugo;
		}
		if (_skillselect._skill_1 == PlayerSkills.skillSet.InstallTower) {
			this.gameObject.GetComponent<Image> ().sprite = tower;
		}
	}
}