using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class menuscript : MonoBehaviour {

	public Canvas quitMenu;
	public Button newGame;
	public Button load;
	public Button option;
	public Button exit;

	// Use this for initialization
	void Start () {
		quitMenu = quitMenu.GetComponent<Canvas> ();
		newGame = newGame.GetComponent<Button> ();
		exit = exit.GetComponent<Button> ();
		load = load.GetComponent<Button> ();
		quitMenu.enabled = false;
	}

	public void ExitPress(){
		quitMenu.enabled = true;
		newGame.enabled = false;
		exit.enabled = false;
	}

	public void NoPress(){
		quitMenu.enabled = false;
		newGame.enabled = true;
		exit.enabled = true;
	}

	public void StartLevel1(){
		Application.LoadLevel (1);
	}

	public void ExitGame(){
		Application.Quit ();
	}

	
}
