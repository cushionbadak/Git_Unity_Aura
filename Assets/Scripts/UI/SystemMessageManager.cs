using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SystemMessageManager :MonoBehaviour{
    private static SystemMessageManager uniqueInstance = null;
    public static SystemMessageManager I { get { return uniqueInstance; } }
    public List<string> mes;
    public int max=5;
    float timeSum=0;
	// Use this for initialization
	void Start () {
        if (uniqueInstance == null)
            uniqueInstance = this;
        else
            Destroy(this.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        timeSum += Time.deltaTime;
        if (mes.Count >= 2)
        {
            if (timeSum >= 1)
            {
                timeSum = 0;
                if (mes.Count != 0)
                    mes.RemoveAt(0);
            }
        }
        
        else
        {
            if (timeSum >= 3)
            {
                timeSum = 0;
                if (mes.Count != 0)
                    mes.RemoveAt(0);
            }
        }

        if(mes.Count==0)
        {
            timeSum = 0;
        }
	}

    public void addMessage(string s)
    {
        if (!isFull())
            mes.Add(s);
        else
        {
            mes.RemoveAt(0);
            mes[mes.Count - 1] = s;
        }
    }

    public bool isFull()
    {
        if (mes.Count >= max+1)//5개까지 허용 가능
            return true;
        else
           return false;
    }
}
