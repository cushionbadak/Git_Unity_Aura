using UnityEngine;
using System.Collections;

public class TotalManager : MonoBehaviour {
	//Singleton
	private static TotalManager uniqueInstance = null;
	public static TotalManager I { get { return uniqueInstance; } }

	public string SceneToStart;

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

		TotalManager.I.currentScene = SceneToStart;

		//test: if PlayerLevelData works well
		//	TotalManager will log 'level 8' Player's maxHP and 'level 20' player's needEXP
		Debug.Log ("Level 8 player's HP : " + PlayerLevelData.I.Status[8].maxHP);
		Debug.Log ("Level 20 player's needEXP : " + PlayerLevelData.I.Status[20].needEXP);
		//test end.
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private string[] Scenes = {"TestScene, MainScreen"};
	private string currentScene;
	public string getCurrentScene(){return currentScene;}
	public void setCurrentScene(string value){
		foreach (string i in Scenes) {
			if(i.Equals (value)){
				currentScene = value;
				Application.LoadLevel (value);
			}
		}
	}
}
