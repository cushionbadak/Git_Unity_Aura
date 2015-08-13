using UnityEngine;
using System.Collections;

public class ItemBoxOpen : MonoBehaviour {
    public int roomNum;
     ItemCreate sc;
    public MapManager mapM;
    public Sprite sp;
    bool one=false;
    // Use this for initialization
    void Start () {
        sc = GetComponent<ItemCreate>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(mapM.getRoomStatus()[roomNum]&&!one)
        {   
            one = true;
            transform.parent.gameObject.GetComponent<SpriteRenderer>().sprite = sp;
            sc.canOpen = true;
        }
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.tag=="PlayerBody")
        {
            if(!mapM.getRoomStatus()[roomNum])
            {

                SystemMessageManager.I.addMessage("방을 클리어해야 열 수 있습니다.");
            }
        }
    }
}
