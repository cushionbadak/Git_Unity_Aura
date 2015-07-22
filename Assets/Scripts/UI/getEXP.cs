using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class getEXP : MonoBehaviour {

    public GameObject player;
    Player pl;
	// Use this for initialization
	void Start () {
        pl = player.GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		string str = pl.EXP.ToString() + "/" + PlayerLevelData.I.Status[pl.level +1].needEXP;
        GetComponent<Text>().text = str;
    }
}
