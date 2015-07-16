using UnityEngine;
using System.Collections;

public class testIfPlayerSound : Player {
	
	//Character, Player 에 정의되어있는 값들.
	/*
	public float maxHP;
	public float currentHP;
	public int level;
	public float originalSpeed;
	public float currentSpeed;

	public void haveKnockback(Vector3 moveVector){}
	public void haveStun(float time){}
	public void haveSnare(float time){}
	public void Die(){}

	public void pause(){}
	public void resume(){}

	public int powerUpPotion;		//파워 업 포션
	public int speedUpPotion;		//스피드 업 포션
	public int rangeUpPotion;		//레인지 업 포션
	//포션 관련 수치
	//예를 들어 powerUpPotionScale = 1.1, powerUpPotion = 2인 경우,
	//Aura의 damage * 1.1 * 1.1 가 실제 데미지로 들어간다.
	public float powerUpPotionScale;
	public float speedUpPotionScale;
	public float rangeUpPotionScale;

	//경험치
	public float EXP;

	//기타 아이템의 보유여부
	public bool isThunderShoes;			//오오라 내의 적 하나에게 일정 시간마다 임의의 적에게 데미지와 경직을 주는 번개를 만든다.								
	public bool isDraculaBrooch;		//오오라 안의 적이 데미지를 입을 때마다 체력이 회복된다.								
	public bool isStickyBall;			//오오라에 적이 닿을 때 잠시 멈추게 한다.								
	public bool isCriticalKnuckle;		//10%확률로 2배의 데미지를 준다.								
	public bool isSpecialThing;	*/
	
	void Start(){
		//위의 변수선언을 전부 주석처리하고도 잘 돌아갑니다.
		//레벨 3에 맞는 값들로 초기화시켜주기.
		maxHP = PlayerLevelData.I.Status[3].maxHP;
		currentHP = maxHP;
		level = PlayerLevelData.I.Status [3].level;
		originalSpeed = 3.0f;
		currentSpeed = 3.0f;
		
		powerUpPotion = 0;
		powerUpPotionScale = 1.2f;
		speedUpPotion = 0;
		speedUpPotionScale = 1.2f;
		rangeUpPotion = 0;
		rangeUpPotionScale = 1.2f;
		isThunderShoes = false;
		isDraculaBrooch = false;
		isStickyBall = false;
		isCriticalKnuckle = false;
		isSpecialThing = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			//AddMore about saving playerinformation
			GameManager.I.savePlayerInfo(2, this);
			Debug.Log ("save completed");
		}
	}
	
	
}
