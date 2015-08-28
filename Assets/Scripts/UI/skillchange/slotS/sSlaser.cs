using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class sSlaser : MonoBehaviour {
	public GameObject skillbringin;
	PlayerSkills _skillselect;
	
	// Use this for initialization
	void Start () {
		_skillselect = skillbringin.GetComponent<PlayerSkills> ();
	}
	
	
	// Update is called once per frame
	void Update () {
	}
	public void clickonbutton(){
		_skillselect._skill_2 = PlayerSkills.skillSet.Laser;
	}
}