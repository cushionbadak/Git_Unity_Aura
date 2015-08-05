using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityStandardAssets.Cameras;
public class ScriptsManager : MonoBehaviour {

    private static ScriptsManager uniqueInstance = null;
    public static ScriptsManager I { get { return uniqueInstance; } }
    public Text n;
    public Text d;
    public GameObject plDum;
    public GameObject plReal;
    public ParseScripts p;
    public GameObject dialogUI;
    public bool scriptMode = true;
    int index = 0;
    int prevIndex = 0;
    // Use this for initialization
    void Start() {
        if (uniqueInstance == null)
            uniqueInstance = this;
        else
            Destroy(this.gameObject);
        scriptChange();
    }

    // Update is called once per frame
    void Update()
    {
        if (scriptMode)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (index < p.dia.Count - 1)
                    index++;
            }


            if (index == prevIndex)
            {

            }
            else
            {
                scriptChange();
                prevIndex = index;
            }
        }
    }

    void scriptChange()
    {
        string sc = p.dia[index].dialog;

        if(sc.Contains("(End)"))
        {
            scriptModeOff();

        }
        if (sc.Contains("(Move)"))
        {
            scriptModeOff();
            int i;
            i = sc.IndexOf(')');

            string[] sp1 = sc.Split(')');
            string[] sp2 = sp1[1].Split(',');

            float x, y, z;

            x = Convert.ToSingle(sp2[0]);
            y = Convert.ToSingle(sp2[1]);
            z = Convert.ToSingle(sp2[2]);
            Vector3 pos = new Vector3(x, y, z);
            
            CutSceneManager.I.Move(pos);
        }

        if(sc.Contains("(!)"))
        {
            scriptModeOff();
            CutSceneManager.I.exclamation();
        }

        if(sc.Contains("(?)"))
        {

            scriptModeOff();
            CutSceneManager.I.question();
        }

        n.text = p.dia[index].name;
        d.text = p.dia[index].dialog;
    }

    public void scriptModeOff()
    {
        scriptMode = false;
        dialogUI.SetActive(false);
    }
    public void scriptInit()
    {
        n.text = p.dia[index].name;
        d.text = p.dia[index].dialog;
    }

    public void scriptModeON()
    {
        index++;
        scriptMode = true;
        dialogUI.SetActive(true);
        scriptChange();
    }

    public void cameraToPlayer()
    {
        GameObject cam = GameObject.Find("Camera");
        cam.GetComponent<AutoCam>().SetTarget(plDum.transform);
    }
   
    public void cameraToDummy()
    {
        GameObject cam = GameObject.Find("Camera");
        cam.GetComponent<AutoCam>().SetTarget(plDum.transform);

    }
   
}
