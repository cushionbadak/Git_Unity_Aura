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