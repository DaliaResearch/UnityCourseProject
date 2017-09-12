﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour {

	public int coinValue;
	public GameObject splosion;

	private LevelManager theLevelManager;

	// Use this for initialization
	void Start () {
		theLevelManager = FindObjectOfType<LevelManager> ();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			theLevelManager.AddCoins (coinValue);
			Destroy (gameObject);
			Instantiate (splosion, gameObject.transform.position, splosion.transform.rotation);
		}
	}
}