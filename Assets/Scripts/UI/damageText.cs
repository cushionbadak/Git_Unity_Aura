using UnityEngine;
using System.Collections;

public class damageText : MonoBehaviour {
   Color c;
	// Use this for initialization
	void Start () {

        c = GetComponent<TextMesh>().color;
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.up*0.03f);
        
        GetComponent<TextMesh>().color -=new Color(0.0f, 0.0f, 0.0f, 0.02f);
	}
}
