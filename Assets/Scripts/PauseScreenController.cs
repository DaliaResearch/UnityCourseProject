using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreenController : MonoBehaviour {

	public GameObject thePauseScreen;
	public LevelManager theLevelManager;
	public PlayerController thePlayer; 

	// Use this for initialization
	void Start () {
		theLevelManager = FindObjectOfType<LevelManager> ();
		thePlayer = FindObjectOfType<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.P)) {
			if (Time.timeScale == 0f) {
				ResumeGame ();
			} else {
				PauseGame ();
			}
		}
	}

	public void PauseGame () {
		thePauseScreen.SetActive (true);
		Time.timeScale = 0f;
		theLevelManager.gameMusic.Stop ();
		thePlayer.canMove = false;
	}

	public void ResumeGame () {
		thePauseScreen.SetActive (false);
		Time.timeScale = 1f;
		theLevelManager.gameMusic.Play ();
		thePlayer.canMove = true;
	}

	public void MainMenu () {
		SceneManager.LoadScene ("MainMenu");
	}
}
