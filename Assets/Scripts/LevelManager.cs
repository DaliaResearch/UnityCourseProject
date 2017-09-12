﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	public float waitToRespawn;
	private PlayerController thePlayer;
	public GameObject deathSplosion;

	public int coinsCount;

	public Text coinsText;

	public Image heart1;
	public Image heart2;
	public Image heart3;

	public Sprite heartFull;
	public Sprite heartHalf;
	public Sprite heartEmpty;

	public int maxHealth;
	public int actualHealth;

	public bool respawing;

	// Use this for initialization
	void Start () {
		thePlayer = FindObjectOfType<PlayerController> ();
		coinsCount = 0;
		UpdateCoinsText ();
		actualHealth = maxHealth;
		UpdateHearts ();
		respawing = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Respawn (){
		StartCoroutine ("RespawnCo");
	}

	public IEnumerator RespawnCo () {
		thePlayer.gameObject.SetActive (false);

		Instantiate (deathSplosion, thePlayer.transform.position, thePlayer.transform.rotation);

		yield return new WaitForSeconds (waitToRespawn);

		actualHealth = maxHealth;
		UpdateHearts ();

		thePlayer.transform.position = thePlayer.respawnPosition;
		thePlayer.gameObject.SetActive (true);

		respawing = false;
	}

	public void AddCoins(int coinsToAdd){
		coinsCount += coinsToAdd;
		UpdateCoinsText ();
	}

	public void AddHurt(int hurtToTake){
		actualHealth -= hurtToTake;
		UpdateHearts ();

		if (actualHealth <= 0 && !respawing) {
			Respawn ();
			respawing = true;
		}
	}

	void UpdateCoinsText (){
		coinsText.text = "Coins: " + coinsCount;
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
}