using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPointController : MonoBehaviour {

	public string leveToLoad;
	public float waitUntilWalk;
	public float waitUntilLoadLevel;
	private bool movingPlayer;

	private PlayerController thePlayer;
	private LevelManager theLevelManager;
	private CameraController theCamera;

	private SpriteRenderer theSpriteRenderer;
	public Sprite flagOpen;

	// Use this for initialization
	void Start () {
		thePlayer = FindObjectOfType<PlayerController> ();
		theCamera = FindObjectOfType<CameraController> ();
		theLevelManager = FindObjectOfType<LevelManager> ();

		movingPlayer = false;
		theSpriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (movingPlayer) {
			thePlayer.myRigidbody.velocity = new Vector3(thePlayer.moveSpeed, thePlayer.myRigidbody.velocity.y, 0f);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			StartCoroutine("EndLevelCo");
		}
	}

	IEnumerator EndLevelCo () {
		// Save the values for the next Level
		PlayerPrefs.SetInt("CoinsCount", theLevelManager.coinsCount);
		PlayerPrefs.SetInt("ActualHealth", theLevelManager.actualHealth);
		PlayerPrefs.SetInt("ActualLifes", theLevelManager.actualLifes);

		theSpriteRenderer.sprite = flagOpen;

		theLevelManager.gameMusic.Stop ();
		theLevelManager.gameOverMusic.Play ();

		thePlayer.myRigidbody.velocity = Vector3.zero;
		thePlayer.canMove = false;
		thePlayer.knockedout = true;
		theCamera.followTarget = false;

		yield return new WaitForSeconds(waitUntilWalk);

		movingPlayer = true;

		yield return new WaitForSeconds(waitUntilLoadLevel);

		SceneManager.LoadScene (leveToLoad);
	}
}
