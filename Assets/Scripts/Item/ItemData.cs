using UnityEngine;
using System.Collections;

public class ItemData :MonoBehaviour {
	public const int size=7;	
	public string[] itemName;
	public Sprite[] sp;
	public string[] description;


	public ItemStruct[] itemList=new ItemStruct[size];

	// Use this for initialization
	void Start () {
		for (int i=0; i<size; i++) {
			itemList[i]=new ItemStruct();
			itemList[i].itemName=itemName[i];
			itemList[i].image=sp[i];
			itemList[i].description=description[i];
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
}
