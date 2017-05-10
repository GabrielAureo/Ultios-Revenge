using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	[SerializeField]
	private Rigidbody2D rb;
	[SerializeField]

	public float walkingSpeed;

	public GameStory story;
	public float health;
	public float damage;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D>();
	}

	void Update(){
		if (story.reading) {
			rb.velocity = Vector2.zero;
			return;
		}

		rb.velocity = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical")) * walkingSpeed;
	}

	

}
