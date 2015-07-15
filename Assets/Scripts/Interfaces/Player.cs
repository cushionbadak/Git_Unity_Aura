using UnityEngine;
using System.Collections;

public class Player : Character{

	//각종 포션의 개수. 기획상 3중첩까지 가능하다.
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
	public int EXP;

	//기타 아이템의 보유여부
	public bool isThunderShoes;			//오오라 내의 적 하나에게 일정 시간마다 임의의 적에게 데미지와 경직을 주는 번개를 만든다.								
	public bool isDraculaBrooch;		//오오라 안의 적이 데미지를 입을 때마다 체력이 회복된다.								
	public bool isStickyBall;			//오오라에 적이 닿을 때 잠시 멈추게 한다.								
	public bool isCriticalKnuckle;		//10%확률로 2배의 데미지를 준다.								
	public bool isSpecialThing;			//오오라의 크기가 150%로 증가하고, Space Bar를 눌러 매우 짧은 쿨타임으로 텔레포트 스킬을 사용할 수 있다.

	//public void haveKnockback(Vector3 moveVector){}
	//public void haveStun(float time){}
	//public void haveSnare(float time){}
	//public void Die(){}
	
	//public void pause(){}
	//public void resume(){}
}
