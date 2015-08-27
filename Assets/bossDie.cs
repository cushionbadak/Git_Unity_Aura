using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class bossDie : MonoBehaviour {
	public GameObject boss;
	Character c;
	public float timeSum;
	public GameObject a;
	public GameObject b;
	public GameObject cc;
	public GameObject d;
	public Player p;
	// Use this for initialization
	void Start () {
	 c = GetComponent<Character> ();
		p = GameObject.FindWithTag ("PlayerBody").GetComponent<Player> ();
	}
	
	// Update is called once per frame
	void Update () {

	if(boss==null)

		{
			timeSum += Time.deltaTime;
			a.SetActive(true);
			b.SetActive(true);
			cc.SetActive(false);
			d.SetActive(false);
			if(timeSum>=5.0f)
			{

				Game.current.hp = p.currentHP;
				Game.current.exp = p.EXP;
				Game.current.level = p.level;
				Game.current.currentChapter = 2;
				List<bool> mapStatus=new List<bool>();
				for (int i = 0; i < 18; i++)
					mapStatus.Add(false);
				mapStatus[0]=true;
				Game.current.roomStatus = mapStatus;
				Game.current.powerUpPotion = p.powerUpPotion;
				Game.current.rangeUpPotion = p.rangeUpPotion;
				Game.current.speedUpPotion = p.speedUpPotion;
				Game.current.isCriticalKnuckle=p.isCriticalKnuckle;
				Game.current.isDraculaBrooch=p.isDraculaBrooch;
				Game.current.isThunderShoes=p.isThunderShoes;
				Game.current.isStickyBall=p.isStickyBall;
				
				Game.current.playerPosition = GameManager.I.findPlayer().gameObject.transform.position;
				Game.current.dialogIndex = 0;
				Game.current.skills = GameManager.I.skills;
				
				Application.LoadLevel(5);
				
			}
		}


	}	
}