using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

	public Transform spinSawDropperLeftPoint;
	public Transform spinSawDropperRightPoint;
	public GameObject spinSawDropper;

	public float spinSawDropperTimeBetweenDrops;
	private float spinSawDropperTimeBetweenDropsCounter;

	public GameObject spinSaw;

	private bool bossActive;

	// Use this for initialization
	void Start () {
		bossActive = false;
		spinSawDropperTimeBetweenDropsCounter = spinSawDropperTimeBetweenDrops;
	}
	
	// Update is called once per frame
	void Update () {
		if (bossActive) {
			spinSawDropperTimeBetweenDropsCounter -= Time.deltaTime;

			if (spinSawDropperTimeBetweenDropsCounter <= 0) {
				DropspinSaw ();
				spinSawDropperTimeBetweenDropsCounter = spinSawDropperTimeBetweenDrops;
			}
		}
	}

	void DropspinSaw () {
		spinSawDropper.transform.position = new Vector3 (Random.Range (spinSawDropperLeftPoint.position.x, spinSawDropperRightPoint.position.x), spinSawDropper.transform.position.y, spinSawDropper.transform.position.z);
		Instantiate (spinSaw, spinSawDropper.transform.position, spinSaw.transform.rotation);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			bossActive = true;
		}
	}
}
