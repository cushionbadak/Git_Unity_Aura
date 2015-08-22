using UnityEngine;
using System.Collections;

public class ItemManager : MonoBehaviour {
	public enum ItemType {TEMPORARY, PERMANENT};

	public ItemType itemType;
    public int itemNum;
    bool onlyOne=true;
    public GameObject eff;
    private GameObject player;
    PlayerUnit playerUnit;
	ItemData itemData;

	void Start()
	{
		itemData = GameObject.Find ("GameData").GetComponent<ItemData> ();
	}

	void OnTriggerEnter(Collider col)
    {
        if(onlyOne)
        if (col.gameObject.tag == "PlayerBody")
        {
                onlyOne = false;
            player = col.gameObject;
            playerUnit = col.gameObject.GetComponent<PlayerUnit>();
            playerUnit.powerUp(playerUnit.powerUpPotion);
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
		if(itemType==ItemType.PERMANENT)
		GameManager.I.addItem (itemData.itemList[itemNum]);
        switch (itemNum)
        {
             case 0:
                {
                    if (playerUnit.powerUpPotion <= 5)
                    {
                        SystemMessageManager.I.addMessage("파워 업 포션을 획득했습니다. 공격력이 20% 증가했습니다.");
                        playerUnit.powerUpPotion++;
                        playerUnit.powerUp(playerUnit.powerUpPotion);
					}
                    else
                    {
                        SystemMessageManager.I.addMessage("최대 한도(5개)를 초과했습니다.");

                    }
                    break;
                }
            case 1:
                {
                    if (playerUnit.speedUpPotion <= 5)
                    {
                        SystemMessageManager.I.addMessage("스피드 업 포션을 획득했습니다. 이동속도가 10% 증가했습니다. ");
                        playerUnit.speedUpPotion++;
                        playerUnit.speedUp(playerUnit.speedUpPotion);
                    }
                    else
                    {
                        SystemMessageManager.I.addMessage("최대 한도(5개)를 초과했습니다.");
                    }
                    break;
                }
            case 2:
                {
                    if (playerUnit.rangeUpPotion <= 5)
                    {
                        SystemMessageManager.I.addMessage("레인지 업 포션을 획득했습니다. 오오라의 크기가 10% 증가했습니다.");
                        playerUnit.rangeUpPotion++;
                        playerUnit.rangeUp(playerUnit.rangeUpPotion);
                    }
                    else
                    {
                        SystemMessageManager.I.addMessage("최대 한도(5개)를 초과했습니다.");
                    }
                   break;
                }

		case 3:
		{
				SystemMessageManager.I.addMessage("HP가 최대로 회복됐습니다.");
				playerUnit.currentHP=playerUnit.maxHP;
	
			break;
		}
        }
    }
}
