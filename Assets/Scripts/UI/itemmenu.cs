using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]

public class itemmenu {
	public string itemName;
	public int itemID;
	public string itemDesc;
	public Texture2D itemIcon;
	public int itemPower;
	public int itemSpeed;

	public ItemType itemType;

	public enum ItemType {
		Weapon,
		Consumable,
		Quest,
	}

	public itemmenu(string name, int id, string desc, int power, int speed, ItemType type){
		itemName = name;
		itemID = id;
		itemDesc = desc;
		itemPower = power;
		itemSpeed = speed;
		itemType = type;
		itemIcon = Resources.Load<Texture2D> ("Item Icons/" + name);
	}
}