using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour {

	public string levelToLoad;
	private bool unlocked;

	public Sprite topOpen;
	public Sprite bottomOpen;
	public Sprite topClosed;
	public Sprite bottomClosed;

	public SpriteRenderer top;
	public SpriteRenderer bottom;

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt (levelToLoad + "Unlocked") == 1) {
			unlocked = true;
		} else {
			unlocked = false;
		}

		if (unlocked) {
			top.sprite = topOpen;
			bottom.sprite = bottomOpen;
		} else {
			top.sprite = topClosed;
			bottom.sprite = bottomClosed;
		}
			
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "Player") {
			if (Input.GetButtonDown ("Jump") && unlocked) {
				SceneManager.LoadScene (levelToLoad);
			}
		}
	}
}
