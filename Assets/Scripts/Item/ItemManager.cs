using UnityEngine;
using System.Collections;

public class ItemManager : MonoBehaviour {
    public int itemNum;
    PlayerUnit playerUnit;
	void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerUnit = col.gameObject.GetComponent<PlayerUnit>();
            applyItem();
            Destroy(this.gameObject);
        }
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
