using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuController : MonoBehaviour {

	private LevelManager theLevelManager;

	// Use this for initialization
	void Start () {
		theLevelManager = FindObjectOfType<LevelManager> ();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RestartLevel () {
		PlayerPrefs.SetInt ("CoinsCount", 0);
		PlayerPrefs.SetInt ("actualLifes", theLevelManager.maxLifes);
		PlayerPrefs.SetInt ("actualHealth", theLevelManager.maxHealth);

		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	public void MainMenu (){
		SceneManager.LoadScene ("MainMenu");
	}
}
