using UnityEngine;
using System.Collections;
// This Script Synchronize Parent's Transform with This object
public class UnitTransformSync : MonoBehaviour 
{
    private bool hasParent = true;

	// Use this for initialization
	void Start () 
    {
        if (transform.parent == null)
        {
            hasParent = false;
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (hasParent)
        {
            transform.parent.position = transform.position;
            //transform.localPosition = new Vector3(0, 0, 0);
        }
	}
}
