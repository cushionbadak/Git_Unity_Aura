using UnityEngine;
using System.Collections;

public class DoorOpenControl : MonoBehaviour {

    public GameObject door;
    bool a;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.tag=="PlayerBody")
        {
            door.GetComponent<ChangeAlpha>().isTrans=true;
        }
    }
}
