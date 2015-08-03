using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ScriptsManager : MonoBehaviour {
    public Text n;
    public Text d;
    public ParseScripts p;
    int index = 0;
    int prevIndex=0;
	// Use this for initialization
	void Start () {
        scriptChange(0);
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(index<p.dia.Count-1)
            index++;
        }
        if(index==prevIndex)
        {

        }
        else
        {
            scriptChange(index);
            prevIndex = index;
        }
    }

    void scriptChange(int index)
    {
        n.text = p.dia[index].name;
        d.text = p.dia[index].dialog;
    }

   
}
