using UnityEngine;
using System.Collections;
using System;

public class SuperboxOpen : MonoBehaviour {
	ItemCreate sc;
	public Sprite sp;
	bool one=false;
	// Use this for initialization
	void Start () {
		sc = GetComponent<ItemCreate>();
		sc.canOpen = true;
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnTriggerEnter(Collider col)
	{
		if(col.tag=="PlayerBody"&&!one)
		{
			one = true;
			transform.parent.gameObject.GetComponent<SpriteRenderer>().sprite = sp;
		}
	}
}