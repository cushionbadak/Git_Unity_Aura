using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class ChildOnOff : MonoBehaviour {
	public List<GameObject> Children;
	// Use this for initialization
	void Start () {
		JustForCount[] objs = GetComponentsInChildren<JustForCount> ();
		for (int i=0; i<objs.Length; i++) {
			Children.Add(objs[i].gameObject);
		}
		off ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void on()
	{
		for(int i=0;i<Children.Count;i++)
		{
			Children[i].SetActive(true);
		}
	}

	public void off()
	{
		for(int i=0;i<Children.Count;i++)
		{
			Children[i].SetActive(false);
		}
	}
}
