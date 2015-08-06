using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityStandardAssets.Cameras;
public class ScriptsManager : MonoBehaviour {

    private static ScriptsManager uniqueInstance = null;
    public static ScriptsManager I { get { return uniqueInstance; } }
    Text n;
    Text d;
    public GameObject BB;
    public GameObject plDum;
    public GameObject[] npcGroup;
    public GameObject plReal;
    public ParseScripts p;
    GameObject dialogUI;
    GameObject inGameUI;
    public bool scriptMode = true;
    int index = 0;
    int prevIndex = 0;
    bool firstFrame = true;
    // Use this for initialization
    void Start() {
        dialogUI = GameObject.FindWithTag("DialogUI");
        inGameUI = GameObject.FindWithTag("InGameUI");
        n = GameObject.FindWithTag("DialogName").GetComponent<Text>();
        d = GameObject.FindWithTag("DialogText").GetComponent<Text>();
        if (uniqueInstance == null)
            uniqueInstance = this;
        else
            Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!firstFrame&&scriptMode)
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
        if (firstFrame)
        {
            scriptChange();
            firstFrame = false;
        }
    }

    void scriptChange()
    {
        string sc = p.dia[index].dialog;

        if (sc.Contains("(End)"))
        {
            scriptOff();
            CutSceneManager.I.doBlack();
        }
        else if (sc.Contains("(Move)"))
        {
            scriptOff();
            if (sc.Contains("/"))
            {
                int temp = sc.IndexOf('/') -1;
                int ind = Convert.ToInt16(sc[temp])-'0';

                string[] sp1 = sc.Split('/');
                
                string[] sp2 = sp1[1].Split(',');

                float x, y, z;

                x = Convert.ToSingle(sp2[0]);
                y = Convert.ToSingle(sp2[1]);
                z = Convert.ToSingle(sp2[2]);
                Vector3 pos = new Vector3(x, y, z);

                CutSceneManager.I.Move(npcGroup[ind],pos);

            }
            else
            {
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
        }

        else if (sc.Contains("(SceneStart)"))
        {
            GameModeOff();
            int i;
            i = sc.IndexOf(')');

            string[] sp1 = sc.Split(')');
            string[] sp2 = sp1[1].Split(',');

            float x, y, z;

            x = Convert.ToSingle(sp2[0]);
            y = Convert.ToSingle(sp2[1]);
            z = Convert.ToSingle(sp2[2]);
            Vector3 pos = new Vector3(x, y, z);
            
            CutSceneManager.I.SceneStart(pos);
        }


        else if (sc.Contains("(GameStart)"))
        {
            int i;
            i = sc.IndexOf(')');

            string[] sp1 = sc.Split(')');
            string[] sp2 = sp1[1].Split(',');

            float x, y, z;

            x = Convert.ToSingle(sp2[0]);
            y = Convert.ToSingle(sp2[1]);
            z = Convert.ToSingle(sp2[2]);
            Vector3 pos = new Vector3(x, y, z);


            plReal.transform.position = pos;
            GameModeOn();
        }

        else if (sc.Contains("(!)"))
        {
            scriptOff();
            if (sc.Trim().EndsWith(")"))
            {
                CutSceneManager.I.exclamation();
            }
            else
            {
                int temp = sc.IndexOf(')') + 2;
                int ind = Convert.ToInt16(sc[temp])-'0';
                CutSceneManager.I.exclamation(npcGroup[ind]);
            }
        }

        else if (sc.Contains("(?)"))
        {
            if (sc.EndsWith(")"))
            {
                scriptOff();
                CutSceneManager.I.question();
            }
            else
            {
                scriptOff();
                int temp = sc.IndexOf(')') + 2;
                int ind = Convert.ToInt32(sc[temp]);
                CutSceneManager.I.question(npcGroup[ind]);
            }
        }

        else
        {
            n.text = p.dia[index].name;
            d.text = p.dia[index].dialog;
        }
    }


    public void scriptMove()
    {
        index++;
        scriptMode = true;
        dialogUI.SetActive(true);
    }

    public void scriptOff()
    {
        scriptMode = false;
        dialogUI.SetActive(false);
    }

    public void GameModeOn()
    {
        GameManager.I.setGameMode(true);
        cameraToPlayer();
        plDum.SetActive(false);
        plReal.SetActive(true);
        foreach (GameObject npc in npcGroup)
        {
            npc.SetActive(false);
        }
        inGameUI.SetActive(true);
        scriptOff();
    }

    public void GameModeOff()
    {
        GameManager.I.setGameMode(false);
        cameraToDummy();
        foreach (GameObject npc in npcGroup)
        {
            npc.SetActive(true);
        }
        plDum.SetActive(true);
        plReal.SetActive(false);
        inGameUI.SetActive(false);
        scriptMode = true;
        dialogUI.SetActive(true);
    }

   

    public void cameraToPlayer()
    {
        GameObject cam = GameObject.Find("Camera");
        cam.GetComponent<AutoCam>().SetTarget(plReal.transform);
    }
   
    public void cameraToDummy()
    {
        GameObject cam = GameObject.Find("Camera");
        cam.GetComponent<AutoCam>().SetTarget(plDum.transform);

    }
   
}
