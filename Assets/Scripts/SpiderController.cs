using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : MonoBehaviour {

	public float moveSpeed;
	private bool canMove;
	private Rigidbody2D theRigidbody;

	// Use this for initialization
	void Start () {
		canMove = false;
		theRigidbody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (canMove) {
			theRigidbody.velocity = new Vector3 (-moveSpeed, theRigidbody.velocity.y, 0f);
		}
	}

	void OnBecameVisible () {
		canMove = true;
	}
		
	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "KillPlane") {
			Destroy (gameObject);
		}
	}

}
