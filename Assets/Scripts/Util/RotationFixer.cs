using UnityEngine;
using System.Collections;

public class RotationFixer : MonoBehaviour
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.localEulerAngles = new Vector3(90, 0, 0);
	}
}
