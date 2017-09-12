using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	public float jumpSpeed;
	private Rigidbody2D myRigibdody;

	public Transform groundCheck;
	public float groundCheckRadious;
	public LayerMask whatIsGround;
	public bool isGrounded;

	private Animator myAnim;

	public Vector3 respawnPosition;

	private LevelManager theLevelManager;

	// Use this for initialization
	void Start () {
		myRigibdody = GetComponent<Rigidbody2D> ();	
		myAnim = GetComponent<Animator> ();
		respawnPosition = transform.position;
		theLevelManager = FindObjectOfType<LevelManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		isGrounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadious, whatIsGround);

		if (Input.GetAxis ("Horizontal") > 0f) {
			myRigibdody.velocity = new Vector3 (moveSpeed, myRigibdody.velocity.y, 0);
			transform.localScale = new Vector3 (1f, 1f, 1f);
		} else if (Input.GetAxis ("Horizontal") < 0f) {
			myRigibdody.velocity = new Vector3 (-moveSpeed, myRigibdody.velocity.y, 0);
			transform.localScale = new Vector3 (-1f, 1f, 1f);
		} else {
			myRigibdody.velocity = new Vector3 (0, myRigibdody.velocity.y, 0);
		}

		if (Input.GetButtonDown ("Jump") && isGrounded) {
			myRigibdody.velocity = new Vector3 (myRigibdody.velocity.x, jumpSpeed, 0);
		}

		myAnim.SetFloat ("Speed", Mathf.Abs(myRigibdody.velocity.x));
		myAnim.SetBool ("Grounded", isGrounded);
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "KillPlane") {
//			gameObject.SetActive (false);
//			transform.position = respawnPosition;
			theLevelManager.Respawn ();
		}

		if (other.tag == "Checkpoint") {
			respawnPosition = other.transform.position;
		}
	}

	void OnCollisionEnter2D (Collision2D collision){
		if (collision.gameObject.tag == "MovingPlatform") {
			transform.parent = collision.transform;
		}
	}

	void OnCollisionExit2D (Collision2D collision){
		if (collision.gameObject.tag == "MovingPlatform") {
			transform.parent = null;
		}
	}
}
