using UnityEngine;
using System.Collections;

public class GoNextScene : MonoBehaviour {
	float TimeSum;
	public int index;
	bool one;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		TimeSum += Time.deltaTime;
		if (TimeSum > 6.0f) {
			if(!one)
			{
				one=true;
				GameObject.Find("black").GetComponent<changeAlpha_sprite>().alpha1();
			}
		}
		if (TimeSum > 10.0f) {
			Application.LoadLevel(index);
		}
	}
}
