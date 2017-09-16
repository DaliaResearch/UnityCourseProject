using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

	public Transform spinSawDropperLeftPoint;
	public Transform spinSawDropperRightPoint;
	public GameObject spinSawDropper;

	public Transform bossLeftPoint;
	public Transform bossRightPoint;

	public Transform cameraPosition;

	public float spinSawDropperTimeBetweenDrops;
	private float spinSawDropperTimeBetweenDropsCounter;

	public float platformsTimeWait;
	private float platformsTimeWaitCounter;

	public GameObject platformsLeft;
	public GameObject platformsRight;
	public GameObject endLevelContainer;
	public CameraController theCamera;

	public GameObject spinSaw;

	public GameObject theBoss;

	private bool bossActive;
	private bool bossDead;
	private bool bossOnTheRight;

	public int bossMaxHealth;
	private int bossActualHealth;

	// Use this for initialization
	void Start () {
		bossActive = false;
		bossDead = false;
		bossOnTheRight = true;
		spinSawDropperTimeBetweenDropsCounter = spinSawDropperTimeBetweenDrops;

		platformsTimeWaitCounter = platformsTimeWait;
		HidePlatforms ();

		bossActualHealth = bossMaxHealth;

		endLevelContainer.SetActive (false);
		theBoss.SetActive (false);

		theCamera = FindObjectOfType<CameraController> ();


	}
	
	// Update is called once per frame
	void Update () {
		if (bossActive) {
			theCamera.transform.position = 
				Vector3.Lerp (
					theCamera.transform.position, 
					new Vector3(cameraPosition.position.x, cameraPosition.position.y, theCamera.transform.position.z), 
					theCamera.smoothing * Time.deltaTime
				);
					
			spinSawDropperTimeBetweenDropsCounter -= Time.deltaTime;

			if (spinSawDropperTimeBetweenDropsCounter <= 0f) {
				DropSpinSaw ();
				spinSawDropperTimeBetweenDropsCounter = spinSawDropperTimeBetweenDrops;
			}

			if (platformsTimeWaitCounter > 0f) {
				platformsTimeWaitCounter -= Time.deltaTime;

				if (platformsTimeWaitCounter <= 0f) {
					ShowPlatforms ();
				}
			}
		}
	}

	void DropSpinSaw () {
		spinSawDropper.transform.position = new Vector3 (Random.Range (spinSawDropperLeftPoint.position.x, spinSawDropperRightPoint.position.x), spinSawDropper.transform.position.y, spinSawDropper.transform.position.z);
		Instantiate (spinSaw, spinSawDropper.transform.position, spinSaw.transform.rotation);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			if (!bossActive && !bossDead) {
				ActiveBoss ();
			}
		}
	}

	void ShowPlatforms (){
		if (bossOnTheRight) {
			platformsLeft.SetActive (true);
		} else {
			platformsRight.SetActive (true);
		}
	}

	void HidePlatforms () {
		platformsLeft.SetActive (false);
		platformsRight.SetActive (false);
	}

	void SpawnBoss (){
		if (bossOnTheRight) {
			theBoss.transform.position = bossRightPoint.position;
		} else {
			theBoss.transform.position = bossLeftPoint.position;
		}
	}

	void ActiveBoss () {
		bossActive = true;
		SpawnBoss ();
		theBoss.SetActive (true);
		theCamera.followTarget = false;
	}

	public void HurtBoss () {
		bossActualHealth -= 1;

		if (bossActualHealth == 0) {
			BossDead ();
		} else {
			bossOnTheRight = !bossOnTheRight;
			SpawnBoss ();

			spinSawDropperTimeBetweenDrops = spinSawDropperTimeBetweenDrops / 2f;

			platformsTimeWaitCounter = platformsTimeWait;
			HidePlatforms ();
		}
	}

	void BossDead () {
		bossDead = true;
		bossActive = false;
		theBoss.SetActive (false);
		HidePlatforms ();
		endLevelContainer.SetActive (true);
		theCamera.followTarget = true;
	}
}
