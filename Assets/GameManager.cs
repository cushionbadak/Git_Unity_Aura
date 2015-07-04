using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	
	
	public GameManager(){
		
	}
	
	/* 모든 HP를 가지는 녀석들이 데미지받으면 HP 닳도록 public void GetDamage(float damage)  메소드를 가져야한다.
	 * 공격하는 주체는 항상 Attack 클래스를 상속받는 녀석이어야 한다.
	 * Attack 클래스는 public float Damagesize() 메소드를 가져서 얘한테 데미지 크기 값을 줘야 한다.
	 ****************/
	public void giveDamage(interfaceCharacter hitThing, interfaceAttack attk){
		hitThing.getDamage(attk.damageSize ());
	}
	/* direction 만큼 물체 position을 움직이기를 요구하는 메소드이다. 꼭 넣어주시길.
	 * 위와 마찬가지로, Attack클래스를 상속받는 녀석만이 
	 ********************/
	public void giveKnockBack(interfaceCharacter hitThing, Vector2 moveVector){
		hitThing.getKnockBack (moveVector);
	}
	/* stunTime만큼 기절하며 스턴받으면 오오라가 그 시간동안 사라졌다 다시 생겨야한다.
	 **************************/
	public void giveStun(interfaceCharacter hitThing, interfaceAttack attk){
		hitThing.getStun (attk.stunTime ());
	}
	/* snareTime만큼 경직에 걸리며 경직에 걸린 캐릭터는 움직일 수 없지만 그동안 오오라는 유지된다.
	 **************/
	public void giveSnare(interfaceCharacter hitThing, interfaceAttack attk){
		hitThing.getSnare (attk.snareTime ());
	}
}

public interface interfaceCharacter {
	void getDamage(float damage);
	void getKnockBack (Vector2 moveVector);
	void getStun(float time);
	void getSnare(float time);
}
public interface interfaceAttack {
	float damageSize();
	float stunTime();
	float snareTime();
}
