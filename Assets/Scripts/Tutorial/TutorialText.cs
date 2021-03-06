﻿using UnityEngine;
using System.Collections;

public class TutorialText : MonoBehaviour {
	public GameObject TextForm;
	public bool one=false;
	public GameObject curText;
	public string message;
	public bool isright;
	int puck=0;
	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "PlayerBody"&&!one) {

			one=true;
			Debug.Log(col.name);
			if(!isright)
			{
			curText=(GameObject)Instantiate(TextForm,this.gameObject.transform.position+Vector3.left,Quaternion.Euler(90,0,0));
			}
			if(isright)
			{
				curText=(GameObject)Instantiate(TextForm,this.gameObject.transform.position+Vector3.right,Quaternion.Euler(90,0,0));
				curText.transform.position+=new Vector3(0,0,-0.3f)*puck;
				puck++;
			}
			string[] mes=message.Split('n');
			message="";
			for(int i=0;i<mes.Length;i++)
			{
				message+=mes[i]+'\n';
			}
			curText.GetComponent<TextMesh>().text=message;

		}
	}

	void OnTriggerExit(Collider col)
	{
		if (col.tag == "PlayerBody") {
			curText.SetActive(false);
			one=false;
		}
	}

}
