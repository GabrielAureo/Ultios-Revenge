using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	[SerializeField]
	private Rigidbody2D rb;
	[SerializeField]
	private GameObject atkHit;

	public float walkingSpeed;

	public GameStory story;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D> ();
		atkHit = transform.FindChild ("Attack Trigger").gameObject;
		atkHit.SetActive (false);
	}

	void Update(){
		if (story.reading) {
			rb.velocity = Vector2.zero;
			return;
		}

		rb.velocity = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical")) * walkingSpeed;

		if (Input.GetButtonDown("Fire1")) {
			atkHit.SetActive(true);
		}
		if (Input.GetButtonUp("Fire1")) {
			atkHit.SetActive (false);
		}
	}


}
