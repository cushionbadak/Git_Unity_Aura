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
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private string[] Scenes = {"Test_MainScreen", "Test_Chapter1", "Test_Chapter2", "Test_Chapter3", "Test_Chapter4"};
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
