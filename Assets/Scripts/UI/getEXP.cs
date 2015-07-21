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
        string str = "EXP : " + pl.EXP.ToString();
        GetComponent<Text>().text = str;
    }
}
