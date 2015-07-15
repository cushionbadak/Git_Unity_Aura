using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class GameManager : MonoBehaviour {

	//Singleton
	private static GameManager uniqueInstance = null;
	public static GameManager I { get { return uniqueInstance; } }

	//temp Player for getPlayerInfo method.
	private Player temp = null;
	
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

		temp = new Player ();

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

	public void savePlayerInfo(int slot, Player player){
		if (slot < 1 || slot > 3) {
			Debug.Log("TotalManager : savePlayerInfo : inappropriate slot number.");
			return;
		}
		string path = Application.dataPath + "/StreamingAssets/PlayerSlot" + slot.ToString() + ".dat";
		StreamWriter sw = new StreamWriter(path, false);
		//글을 적는 순서는 다음과 같다.
		//TotalManager class에서:
		//getCurrentScene
		//
		//Player class에서:
		//level, currentHP, originalSpeed,
		//powerUpPotion, speedUpPotion, rangeUpPotion,
		//powerUpPotionScale, speedUpPotionScale, rangeUpPotionScale,
		//EXP, 
		//isThunderShoes, isDraculaBrooch, isStickyBall, isCriticalKnuckle, isSpecialThing
		//이 순서는 아직 변경 가능성이 있으며, 특히 맵 관련해서는 더욱 변경될 것이다.
		sw.WriteLine(TotalManager.I.getCurrentScene());
		sw.WriteLine (player.level.ToString());
		sw.WriteLine (player.currentHP.ToString ());
		sw.WriteLine (player.originalSpeed.ToString());
		sw.WriteLine (player.powerUpPotion.ToString ());
		sw.WriteLine (player.speedUpPotion.ToString());
		sw.WriteLine (player.rangeUpPotion.ToString());
		sw.WriteLine (player.powerUpPotionScale.ToString());
		sw.WriteLine (player.speedUpPotionScale.ToString ());
		sw.WriteLine (player.rangeUpPotionScale.ToString ());
		sw.WriteLine (player.EXP.ToString());

		sw.WriteLine (player.isThunderShoes.ToString());
		sw.WriteLine (player.isDraculaBrooch.ToString());
		sw.WriteLine (player.isStickyBall.ToString ());
		sw.WriteLine (player.isCriticalKnuckle.ToString());
		sw.WriteLine (player.isSpecialThing.ToString ());

		sw.Close ();
	}

	public Player getPlayerInfo(int slot, Player player){
		//slot num이 1~3 이 아니면 에러가 날 가능성이 높습니다.
		string path = Application.dataPath + "/StreamingAssets/PlayerSlot" + slot.ToString() + ".dat";

		string[] trash = new string[1];
		StreamReader sr = new StreamReader (path);

		trash [0] = sr.ReadLine ();									//1
		temp.level = Convert.ToInt32 (sr.ReadLine ());				//2
		temp.currentHP = Convert.ToSingle (sr.ReadLine());			//3
		temp.originalSpeed = Convert.ToSingle (sr.ReadLine());		//4
		temp.powerUpPotion = Convert.ToInt32 (sr.ReadLine());		//5
		temp.speedUpPotion = Convert.ToInt32 (sr.ReadLine());		//6
		temp.rangeUpPotion = Convert.ToInt32 (sr.ReadLine());		//7
		temp.powerUpPotionScale = Convert.ToSingle (sr.ReadLine ());	//8
		temp.speedUpPotionScale = Convert.ToSingle (sr.ReadLine ());	//9
		temp.rangeUpPotionScale = Convert.ToSingle (sr.ReadLine ());	//10

		temp.EXP = Convert.ToInt32 (sr.ReadLine ());					//11
		temp.isThunderShoes = Convert.ToBoolean (sr.ReadLine());		//12
		temp.isDraculaBrooch = Convert.ToBoolean (sr.ReadLine());		//13
		temp.isStickyBall = Convert.ToBoolean (sr.ReadLine ());			//14
		temp.isCriticalKnuckle = Convert.ToBoolean (sr.ReadLine ());	//15
		temp.isSpecialThing = Convert.ToBoolean (sr.ReadLine ());		//16

		sr.Close ();

		//파일에 들어있지 않은 값들은 이 밑에서 계산한다.
		temp.maxHP = PlayerLevelData.I.Status [temp.level].maxHP;
		//speedUpPotion에 따른 currentSpeed변화를 여기서 전부 계산한다.
		temp.currentSpeed = temp.originalSpeed * Convert.ToSingle(Math.Pow(Convert.ToDouble (temp.rangeUpPotionScale), Convert.ToDouble(temp.rangeUpPotion)));

		return temp;
	}



}