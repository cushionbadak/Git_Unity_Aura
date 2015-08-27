using UnityEngine;
using System.Collections;

public class NewPlayerData : SingleTonBehaviour<NewPlayerData> {

	public struct PlayerStatus{
		public int Level;
        public float HPMax, Damage;
        public int ExpRequired, ExpTotal;

		public PlayerStatus(int Level, float HPMax, float Damage, int ExpRequired, int ExpTotal)
        {
			this.Level = Level;
			this.HPMax = HPMax;
			this.Damage = Damage;
			this.ExpRequired = ExpRequired;
			this.ExpTotal = ExpTotal;
		}
	}

	public PlayerStatus[] LevelData = new PlayerStatus[40];


	void Awake() {
		// Generate LevelData
		LevelData[0] = new PlayerStatus(0, 10000.0f, 10000.0f, 0, 0);
		LevelData[1] = new PlayerStatus(1, 100.0f, 10.0f, 0, 0);
		LevelData[2] = new PlayerStatus(2, 120.0f, 10.0f, 20, 20);

		for (int i = 3; i < LevelData.Length; ++i) {
			float nextHPMax = LevelData[i - 1].HPMax + 20;
			float nextDamage = LevelData[i - 1].Damage;
			int nextExpRequired = 2 * LevelData[i - 1].ExpRequired - LevelData[i - 2].ExpRequired + 20;
			int nextExpTotal = LevelData[i - 1].ExpTotal + nextExpRequired;
			
			LevelData[i] = new PlayerStatus(i, nextHPMax, nextDamage, nextExpRequired, nextExpTotal);
		}
	}
}
