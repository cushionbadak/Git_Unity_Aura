using UnityEngine;
using System.Collections;

public class ItemCreate : MonoBehaviour {
    public GameObject[] Items;
    public Sprite open;
    bool isOpen=false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnTriggerEnter(Collider col)
    {
        if(col.tag=="PlayerBody"&&!isOpen  )
        {

            int index = Random.Range(0, Items.Length);
            GameObject i = Items[index];
            GameObject Item = (GameObject)GameObject.Instantiate(i, transform.position+Vector3.right*2, Quaternion.Euler(90, 0 , 0));
            transform.parent.gameObject.GetComponent<SpriteRenderer>().sprite=open;
            Destroy(transform.parent.gameObject, 5.0f);
            isOpen = true;
        }
    }
}
