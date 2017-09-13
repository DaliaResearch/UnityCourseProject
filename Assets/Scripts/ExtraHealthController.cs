using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraHealthController : MonoBehaviour {

	private LevelManager theLevelManager;
	public int healthToGive;

	// Use this for initialization
	void Start () {
		theLevelManager = FindObjectOfType<LevelManager> ();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D other){
		theLevelManager.AddExtraHealth (healthToGive);
		Destroy (gameObject);
	}

}
