using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompBoxController : MonoBehaviour {

	public GameObject enemySplosion;
	public float bounceImpulse;
	private Rigidbody2D thePlayerRigidbody;

	// Use this for initialization
	void Start () {
		thePlayerRigidbody = transform.parent.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Enemy") {
			other.gameObject.SetActive (false);
			thePlayerRigidbody.velocity = new Vector3 (thePlayerRigidbody.velocity.x, bounceImpulse, 0f);
			Instantiate (enemySplosion, other.transform.position, enemySplosion.transform.rotation);
		}
	}
}
