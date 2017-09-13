using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetOnRespawn : MonoBehaviour {

	private Vector3 startPosition;
	private Vector3 startLocalScale;
	private Quaternion startRotation;

	private Rigidbody2D myRigidbody;

	// Use this for initialization
	void Start () {
		startPosition = transform.position;
		startLocalScale = transform.localScale;
		startRotation = transform.rotation;

		myRigidbody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ResetObject (){
		gameObject.SetActive (true);

		transform.position = startPosition;
		transform.localScale = startLocalScale;
		transform.rotation = startRotation;

		if (myRigidbody != null) {
			myRigidbody.velocity = Vector3.zero;
		}
	}
}
