﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeAlpha_ImageUI : MonoBehaviour {
	
	public bool isTransparent=false;
	Color c1,c2;
	float timeSum1=0;
	float timeSum2=0;
	float ratio=2;
	// Use this for initialization
	void Start () {
		c1 = GetComponent<Image> ().color;
		c2 = GetComponent<Image> ().color;
		c2.a = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (isTransparent) {
			timeSum2=0;
			timeSum1+=Time.deltaTime;
			GetComponent<Image>().color=Color.Lerp(c1,c2,timeSum1/ratio);
		}
		
		
		if (!isTransparent) {
			timeSum1=0;
			timeSum2+=Time.deltaTime;
			GetComponent<Image>().color=Color.Lerp(c2,c1,timeSum2/ratio);
		}
	}
	
	public void alpha0()
	{
		isTransparent = true;
	}
	
	public void alpha1()
	{
		isTransparent = false;
	}
}