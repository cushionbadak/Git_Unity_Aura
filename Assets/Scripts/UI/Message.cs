using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Message : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        string str;
        str = "";
        foreach(string s in SystemMessageManager.I.mes)
        {
            str = str + s + "\n";
        }
        this.GetComponent<Text>().text = str;
	}
}
