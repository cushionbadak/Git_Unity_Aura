﻿using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class DialogStruct
{
    public string name;
    public string dialog;
} 

public class ParseScripts : MonoBehaviour {
    public string file_path;
    public StreamWriter st_w;
    public StreamReader st_r;
    public FileStream fs_w;
    public FileStream fs_r;
    public string tempStr;
    public List<DialogStruct> dia;
    // Use this for initialization
    void Awake () {
        dia = new List<DialogStruct>();
        file_path = "Assets/Resources/Dialogs/prologue.txt";
        st_r = new StreamReader(file_path);

        while(st_r.Peek()>-1)
        {
            DialogStruct ds=new DialogStruct();
            tempStr =st_r.ReadLine();
            tempStr.Trim();
            if (tempStr.Contains(":"))
            {
                string[] sp=tempStr.Split(':');
                ds.name = sp[0].Trim();
                ds.dialog = sp[1].Trim();
            }
            else
            {
                ds.name = "";
                ds.dialog = tempStr;
            }
            dia.Add(ds);
        }
        st_r.Close();
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}