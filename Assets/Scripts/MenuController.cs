using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	public string firstLevel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void NewGame () {
		PlayerPrefs.DeleteAll ();

		SceneManager.LoadScene (firstLevel);}

	public void ContinueGame () {

		SceneManager.LoadScene ("LevelSelector");
	}

	public void Quit () {
		Application.Quit ();
	}
}
