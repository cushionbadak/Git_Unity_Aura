using UnityEngine;
using System.Collections;

public class pausemenu : MonoBehaviour {
	public bool isPaused;
	public GameObject pauseMenuCanvas;
	public GameObject statusCanvas;
	public GameObject itemCanvas;
	public GameObject skillCanvas;
	public GameObject configCanvas;
	public string status;
	public string item;
	public string skill;
	public string config;

	// Update is called once per frame
	void Update () {
		if (isPaused) {
			pauseMenuCanvas.SetActive (true);
			Time.timeScale = 0f;
		} else {
			pauseMenuCanvas.SetActive (false);
			statusCanvas.SetActive(false);
			itemCanvas.SetActive(false);
			skillCanvas.SetActive(false);
			configCanvas.SetActive(false);
            if(GameManager.I.isPlayerLive)
            {

                Time.timeScale = 1f;
            }
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			isPaused = !isPaused;
			statusCanvas.SetActive(true);
		}
	}

	public void Resume(){
		isPaused = false;
	}

	public void Status(){
		statusCanvas.SetActive (true);
		itemCanvas.SetActive (false);
		skillCanvas.SetActive (false);
		configCanvas.SetActive (false);
	}
	public void Item(){
		statusCanvas.SetActive (false);
		itemCanvas.SetActive (true);
		skillCanvas.SetActive (false);
		configCanvas.SetActive (false);
	}
	public void Skill(){
		statusCanvas.SetActive (false);
		itemCanvas.SetActive (false);
		skillCanvas.SetActive (true);
		configCanvas.SetActive (false);
	}

	public void Config(){
		statusCanvas.SetActive (false);
		itemCanvas.SetActive (false);
		skillCanvas.SetActive (false);
		configCanvas.SetActive (true);
	}
	public void BackToMenu(){
		Application.LoadLevel (0);
	}
}
