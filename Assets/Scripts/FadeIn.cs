using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {
	public Image image;
	public float fadeInTime;

	// Use this for initialization
	void Start () {
		image.gameObject.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
		image.CrossFadeAlpha (0f, fadeInTime, false);

		if (image.color.a == 0) {
			gameObject.SetActive (false);
		}
	}
}
