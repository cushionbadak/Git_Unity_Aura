using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]

public class Game  {
    public static Game current;
    public float hp;
    public int level;
    public int exp;
    public int powerUpPotion;       
    public int speedUpPotion;       
    public int rangeUpPotion;
	public bool isThunderShoes;			//오오라 내의 적 하나에게 일정 시간마다 임의의 적에게 데미지와 경직을 주는 번개를 만든다.								
	public bool isDraculaBrooch;		//오오라 안의 적이 데미지를 입을 때마다 체력이 회복된다.								
	public bool isStickyBall;			//오오라에 적이 닿을 때 잠시 멈추게 한다.								
	public bool isCriticalKnuckle;		//10%확률로 2배의 데미지를 준다.			

	public PlayerSkills.skillSet skill1;
	public PlayerSkills.skillSet skill2;
	public PlayerSkills.skillSet skill3;


    public int dialogIndex;
    public List<PlayerSkills.skillSet> skills;
    private WB_Vector3 _someVector3Info;
    public Vector3 playerPosition
    {
        get
        {
            if (_someVector3Info == null)
            {
                return Vector3.zero;
            }
            else
            {
                return (Vector3)_someVector3Info;
            }
        }
        set
        {
            _someVector3Info = (WB_Vector3)value;
        }
    }

    public int currentChapter;
    public List<bool> roomStatus;

}
