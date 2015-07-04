using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	
	
	public GameManager(){
		//void
	}
	

	public void giveAttack(CharacterSkeleton hitThing, AttackSkeleton attk){
		//enemy가 맞을 할 Attack객체인 경우.
		//player상태를 고려해야 해서 코드가 길어진다.
		if(hitThing.tag == "enemy" && attk.isAttackToEnemy) {
			//플레이어가 만든 attack 이 enemy에 맞는 경우.
			if(attk.isAttackFromPlayer)
				giveDamageToEnemyFromPlayer (hitThing, attk);
			//enemy가 만든 attack이 enemy에 맞는 경우.
			//메소드 이름이 좀 이상해도 넘어가자.
			else giveDamageToPlayer (hitThing, attk);
		}

		//player가 맞을 Attack 객체인 경우.
		//player가 만든 attack이 player를 때릴 리가 없다고 본다.
		//고려할것이 적다.
		if(hitThing.tag == "player" && attk.isAttackToPlayer) {
			giveDamageToPlayer (hitThing, attk);
		}
	}
	/*
	//고려할게 많아서 잠시 빼둠.
	public void giveKnockBack(CharacterSkeleton hitThing, AttackSkeleton attk){
	}
	public void giveStun(CharacterSkeleton hitThing, AttackSkeleton attk){
		hitThing.getStun (attk.stunTime);
	}
	public void giveSnare(CharacterSkeleton hitThing, AttackSkeleton attk){
		hitThing.getSnare (attk.snareTime);
	}
	*/

	public void giveDamageToEnemyFromPlayer(CharacterSkeleton hitThing, AttackSkeleton attk){
		float tmpDamage = attk.damage * playerDamageBuff ();
		//이것도 아직 playerDamageBuff() 매개변수 종류를 모르겠어서 모르겠다.
		//playerDamageBuff()가 구체적으로 완성되면 적어야한다.
		float tmp = hitThing.currentHP - tmpDamage;
		if (tmp > 0)
			hitThing.currentHP = tmp;
		else
			hitThing.Die ();
	}
	public void giveDamageToPlayer(CharacterSkeleton hitThing, AttackSkeleton attk){
		float tmp = hitThing.currentHP - attk.damage;
		if (tmp > 0)
			hitThing.currentHP = tmp;
		else
			hitThing.Die ();
	}
	public static float playerDamageBuff(){
		//player 객체가 매개변수로 들어와야하는데...이걸 어쩌지....?
		//abstract class를 상속받는 interface를 만들어야하나....?
		//아냐 호출 자체가 어려워.....이런!젠장! 이건 내일 물어봐야지.
		//일단 return 0.0f 간다.
		return 0.0f;
	}
}

public abstract class CharacterSkeleton : MonoBehaviour{
	//모든 캐릭터들이 가져야 할 기본적인 속성이다.
	public float maxHP;
	public float currentHP;
	public int level;
	public float originalSpeed;
	public float currentSpeed;
	//마찬가지로 모든 캐릭터들이 가져야 할 기본적인 메소드들.
	//넉백은 캐릭터가 vector3 힘을 받아야한다.
	public abstract void getKnockBack (Vector3 moveVector);
	public abstract void getStun(float time);
	public abstract void getSnare(float time);
	public abstract void Die();
}

public abstract class PlayerSkeleton : CharacterSkeleton {
	/****************************************
		이 클래스를 상속받는 클래스의 tag는 무조건 "player" 일 것을 요구한다.
		그 이외의 경우 결과를 책임지지 않는다.
	 ***************************************/
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

	//기타 아이템의 보유여부
	public bool isThunderShoes;		//오오라 내의 적 하나에게 일정 시간마다 임의의 적에게 데미지와 경직을 주는 번개를 만든다.								
	public bool isDraculaBrooch;	//오오라 안의 적이 데미지를 입을 때마다 체력이 회복된다.								
	public bool isStickyBall;		//오오라에 적이 닿을 때 잠시 멈추게 한다.								
	public bool isCriticalKnuckle;	//10%확률로 2배의 데미지를 준다.								
	public bool isSpecialThing;		//오오라의 크기가 150%로 증가하고, Space Bar를 눌러 매우 짧은 쿨타임으로 텔레포트 스킬을 사용할 수 있다.
}

public abstract class AttackSkeleton : MonoBehaviour{
	public float damage;
	public float stunTime;
	public float snareTime;
	public bool isAttackToPlayer;
	public bool isAttackToEnemy;
	public bool isAttackFromPlayer;
}
