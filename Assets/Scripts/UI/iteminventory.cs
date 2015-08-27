using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class iteminventory : MonoBehaviour {
	public List<itemmenu> inventory = new List<itemmenu>();
	private ItemDatabase database;

	// Use this for initialization
	void Start () {
		database = GameObject.FindGameObjectWithTag ("ItemDatabase").GetComponent<ItemDatabase> ();

		inventory.Add (database.items[0]);

	}


	// Update is called once per frame
	void Update () {
	
	}
}
