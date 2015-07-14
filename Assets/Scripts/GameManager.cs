using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	//Singleton
	private static GameManager uniqueInstance = null;
	public static GameManager I { get { return uniqueInstance; } }
	
	void Awake(){
		DontDestroyOnLoad (this.gameObject);
	}
	
	// Use this for initialization
	void Start () {
		//Singleton
		if (uniqueInstance == null)
			uniqueInstance = this;
		else
			Destroy (this.gameObject);
	}


	public void attackToPlayer(Attack attk, Player player){
		//player가 효과를 받는 attack이 일어났을 때 일어날 일들 중
		//데미지, 넉백, 기절, 경직 정보를 읽어서 player에게 넘겨준다.
		//만약 데미지를 받은 뒤 플레이어가 죽을 체력이면 Die함수를 호출한다.
		//스턴과 경직 둘 다 있는 경우 스턴만 적용된다.
		float tempHP = player.currentHP - attk.damage;
		if (tempHP <= 0)
			TotalManager.I.PlayerDie ();
		else {
			player.haveDamage (attk.damage);
			if (attk.isknockbackVectorNeed) {
				player.haveKnockback (attk.knockbackVector * attk.knockbackForce);
			} else {
				Vector3 temp = player.transform.position - attk.transform.position;
				temp.Normalize();
				player.haveKnockback(temp * attk.knockbackForce);
			}
			if(attk.stunTime > 0.0f)
				player.haveStun(attk.stunTime);
			else if(attk.snareTime > 0.0f)
				player.haveSnare (attk.snareTime);
		}
	}//attackToPlayer End.

	public void attckToEnemy(Attack attk, Character character){
		float tempHP = character.currentHP - attk.damage;
		if (tempHP <= 0) {
			//Please Add More
		}

		//attackToPlayer과 같다.
		/*
		float tempHP = character.currentHP - attk.damage;
		character.currentHP = (tempHP <= 0) ? 0.0f : tempHP;
		if (tempHP <= 0)
			character.Die ();
		else {
			if(attk.knockbackVector.magnitude > 0.0f)
				character.haveKnockback(attk.knockbackVector);
			if(attk.stunTime > 0.0f)
				character.haveStun(attk.stunTime);
			else if(attk.snareTime > 0.0f)
				character.haveSnare(attk.snareTime);

		}
		*/
	}//attackToEnemy End.
}