using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	public float waitToRespawn;
	private PlayerController thePlayer;
	public GameObject deathSplosion;
	public GameObject hurtSplosion;

	public int coinsCount;
	private int coinsExtraLifeCounter;
	public int coinsForExtraLife;

	public Text coinsText;
	public Text lifesText;

	public Image heart1;
	public Image heart2;
	public Image heart3;

	public Sprite heartFull;
	public Sprite heartHalf;
	public Sprite heartEmpty;

	public int maxHealth;
	public int actualHealth;

	public bool respawing;

	public int maxLifes;
	public int actualLifes;

	private ResetOnRespawn[] objectsToReset;

	public GameObject gameOverScreen;

	public AudioSource hurtPlayerSound;
	public AudioSource pickCoinSound;
	public AudioSource gameMusic;
	public AudioSource gameOverMusic;

	// Use this for initialization
	void Start () {
		// Level1 is always unlocked
		PlayerPrefs.SetInt ("Level1Unlocked", 1);

		thePlayer = FindObjectOfType<PlayerController> ();

		respawing = false;

		objectsToReset = FindObjectsOfType<ResetOnRespawn> ();

		coinsCount = 0;
		actualHealth = maxHealth;
		actualLifes = maxLifes;

		GetPlayerPrefs ();

		UpdateHearts ();
		UpdateLifesText ();
		UpdateCoinsText ();
	}

	void GetPlayerPrefs () {
		if (PlayerPrefs.HasKey ("CoinsCount")) {
			coinsCount = PlayerPrefs.GetInt ("CoinsCount");
		}

		if (PlayerPrefs.HasKey ("ActualHealth")) {
			actualHealth = PlayerPrefs.GetInt ("ActualHealth");
		}

		if (PlayerPrefs.HasKey ("ActualLifes")) {
			actualLifes = PlayerPrefs.GetInt ("ActualLifes");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Respawn (){
		actualLifes -= 1;
		UpdateLifesText ();

		if (actualLifes > 0) {
			StartCoroutine ("RespawnCo");
		} else {
			gameMusic.Stop ();
			gameOverMusic.Play ();
			gameOverScreen.SetActive (true);
			thePlayer.gameObject.SetActive (false);
		}
	}

	public IEnumerator RespawnCo () {
		thePlayer.gameObject.SetActive (false);

		Instantiate (deathSplosion, thePlayer.transform.position, thePlayer.transform.rotation);

		yield return new WaitForSeconds (waitToRespawn);

		actualHealth = maxHealth;
		UpdateHearts ();

		coinsCount = 0;
		UpdateCoinsText ();

		thePlayer.transform.position = thePlayer.respawnPosition;
		thePlayer.gameObject.SetActive (true);

		respawing = false;
		thePlayer.knockedout = false;

		foreach (ResetOnRespawn objectToReset in objectsToReset) {
			objectToReset.ResetObject ();
		}
	}

	public void AddCoins(int coinsToAdd){
		pickCoinSound.Play ();
		coinsCount += coinsToAdd;
		UpdateCoinsText ();

		coinsExtraLifeCounter += coinsToAdd;
		if (coinsExtraLifeCounter >= coinsForExtraLife) {
			AddExtraLife ();
			coinsExtraLifeCounter -= coinsForExtraLife;
		}
	}

	public void AddHurt(int hurtToTake){
		if (!thePlayer.knockedout) {
			hurtPlayerSound.Play ();

			thePlayer.Knockout ();

			Instantiate (hurtSplosion, thePlayer.transform.position, hurtSplosion.transform.rotation);

			actualHealth -= hurtToTake;
			UpdateHearts ();

			if (actualHealth <= 0 && !respawing) {
				Respawn ();
				respawing = true;
			}
		}
	}

	void UpdateCoinsText (){
		coinsText.text = "Coins: " + coinsCount;
	}

	void UpdateLifesText (){
		lifesText.text = "Lifes x " + actualLifes;
	}


	void UpdateHearts (){
		switch (actualHealth) {
		case 6:
			heart1.sprite = heartFull;
			heart2.sprite = heartFull;
			heart3.sprite = heartFull;
			break;
		case 5:
			heart1.sprite = heartFull;
			heart2.sprite = heartFull;
			heart3.sprite = heartHalf;
			break;
		case 4:
			heart1.sprite = heartFull;
			heart2.sprite = heartFull;
			heart3.sprite = heartEmpty;
			break;
		case 3:
			heart1.sprite = heartFull;
			heart2.sprite = heartHalf;
			heart3.sprite = heartEmpty;
			break;
		case 2:
			heart1.sprite = heartFull;
			heart2.sprite = heartEmpty;
			heart3.sprite = heartEmpty;
			break;
		case 1:
			heart1.sprite = heartHalf;
			heart2.sprite = heartEmpty;
			heart3.sprite = heartEmpty;
			break;
		case 0:
			heart1.sprite = heartEmpty;
			heart2.sprite = heartEmpty;
			heart3.sprite = heartEmpty;
			break;
		}
	}

	public void AddExtraLife(){
		pickCoinSound.Play ();
		actualLifes += 1;
		UpdateLifesText ();
	}

	public void AddExtraHealth (int healthToGive){
		pickCoinSound.Play ();
		actualHealth += healthToGive;

		if (actualHealth > maxHealth) {
			actualHealth = maxHealth;
		}

		UpdateHearts ();
	}
}
