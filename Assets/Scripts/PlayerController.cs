using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	private float actualMoveSpeed;
	public float moveSpeedModifierOnPlatform;
	private bool onPlatform;

	public float jumpSpeed;
	public Rigidbody2D myRigidbody;

	public Transform groundCheck;
	public float groundCheckRadious;
	public LayerMask whatIsGround;
	public bool isGrounded;

	private Animator myAnim;

	public Vector3 respawnPosition;

	private LevelManager theLevelManager;

	public GameObject stompBox;

	public bool knockedout;
	public float knockoutForce;
	public float knockoutTime;

	public AudioSource jumpSound;

	public bool canMove;

	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();	
		myAnim = GetComponent<Animator> ();
		respawnPosition = transform.position;
		theLevelManager = FindObjectOfType<LevelManager> ();

		canMove = true;
	}
	
	// Update is called once per frame
	void Update () {
		isGrounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadious, whatIsGround);

		if (onPlatform) {
			actualMoveSpeed = moveSpeed * moveSpeedModifierOnPlatform;
		} else {
			actualMoveSpeed = moveSpeed;
		}

		if (canMove) {
			PlayerMovement ();
		}

		myAnim.SetFloat ("Speed", Mathf.Abs (myRigidbody.velocity.x));
		myAnim.SetBool ("Grounded", isGrounded);

		if (myRigidbody.velocity.y < 0) {
			stompBox.SetActive (true);
		} else {
			stompBox.SetActive (false);
		}
	}

	void PlayerMovement() {
		if (Input.GetAxis ("Horizontal") > 0f) {
			myRigidbody.velocity = new Vector3 (actualMoveSpeed, myRigidbody.velocity.y, 0);
			transform.localScale = new Vector3 (1f, 1f, 1f);
		} else if (Input.GetAxis ("Horizontal") < 0f) {
			myRigidbody.velocity = new Vector3 (-actualMoveSpeed, myRigidbody.velocity.y, 0);
			transform.localScale = new Vector3 (-1f, 1f, 1f);
		} else {
			myRigidbody.velocity = new Vector3 (0, myRigidbody.velocity.y, 0);
		}

		if (Input.GetButtonDown ("Jump") && isGrounded) {
			jumpSound.Play ();
			myRigidbody.velocity = new Vector3 (myRigidbody.velocity.x, jumpSpeed, 0);
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "KillPlane") {
			theLevelManager.Respawn ();
		}

		if (other.tag == "Checkpoint") {
			respawnPosition = other.transform.position;
		}
	}

	void OnCollisionEnter2D (Collision2D collision){
		if (collision.gameObject.tag == "MovingPlatform") {
			transform.parent = collision.transform;
			onPlatform = true;
		}
	}

	void OnCollisionExit2D (Collision2D collision){
		if (collision.gameObject.tag == "MovingPlatform") {
			transform.parent = null;
			onPlatform = false;
		}
	}

	public void Knockout (){
		StartCoroutine ("KnockoutCo");
	}

	public IEnumerator KnockoutCo (){
		Debug.Log ("KnockoutCo : INI");

		knockedout = true;
		myRigidbody.AddForce (Vector3.up * knockoutForce, ForceMode2D.Impulse);

		yield return new WaitForSeconds (knockoutTime);

		knockedout = false;

		Debug.Log ("KnockoutCo : END");
	}

}
