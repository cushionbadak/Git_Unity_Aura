using UnityEngine;
using System.Collections;

public class ItemManager : MonoBehaviour {
    public int itemNum;
    public GameObject eff;
    public GameObject player;
    PlayerUnit playerUnit;
	void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            player = col.gameObject;
            playerUnit = col.gameObject.GetComponent<PlayerUnit>();
            applyItem();
            StartCoroutine("effect");
            Destroy(this.gameObject);
        }
    }

    IEnumerator effect()
    {
        GameObject obj=(GameObject)Instantiate(eff, player.transform.position, Quaternion.identity);
        
        obj.transform.parent = player.transform;
        obj.transform.position = player.transform.position+Vector3.up;

        eff.GetComponent<ParticleSystem>().Play(true);
        Destroy(obj, 10.0f);
        yield return null;
    }

    void applyItem()
    {
        switch (itemNum)
        {
             case 1:
                {
                    if (playerUnit.powerUpPotion < 3)
                        playerUnit.powerUpPotion++;
                    break;
                }
            case 2:
                {
                    if (playerUnit.speedUpPotion < 3)
                        playerUnit.speedUpPotion++;
                    break;
                }
            case 3:
                {
                    if (playerUnit.rangeUpPotion < 3)
                        playerUnit.rangeUpPotion++;
                   break;
                }
        }
    }
}
