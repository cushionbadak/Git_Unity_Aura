using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class sSteleport : MonoBehaviour {
	public Button button;
	public GameObject skillbringin;
	PlayerSkills _skillselect;
	
	// Use this for initialization
	void Start () {
		_skillselect = skillbringin.GetComponent<PlayerSkills> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			_skillselect._skill_2 = PlayerSkills.skillSet.Teleport;
		}
	}
}
