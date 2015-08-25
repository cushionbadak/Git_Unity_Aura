using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CheckCircle : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void on()
	{
		Color c;
		c = this.gameObject.GetComponent<Image> ().color;
		c.a = 1;
		this.gameObject.GetComponent<Image> ().color = c;
	}
	
	public void off()
	{
		Color c;
		c = this.gameObject.GetComponent<Image> ().color;
		c.a = 0;
		this.gameObject.GetComponent<Image> ().color = c;
	}
}