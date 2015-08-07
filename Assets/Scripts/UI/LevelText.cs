using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelText : MonoBehaviour {

    GameObject player;
    Player pl;
    // Use this for initialization
    void Start () {
        player = GameObject.FindWithTag("PlayerBody");
        pl = player.GetComponent<Player>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        string str = pl.level.ToString();
        GetComponent<Text>().text = str;
    }
}
