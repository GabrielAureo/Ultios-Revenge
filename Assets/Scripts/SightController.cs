using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightController : MonoBehaviour {
	private Collider2D radius;

	// Use this for initialization
	void Start () {
		radius = gameObject.GetComponent<CircleCollider2D>();
		Physics2D.IgnoreCollision (radius, transform.root.GetComponent<Collider2D>());
		Physics2D.IgnoreCollision (radius, transform.root.FindChild("Hit Trigger").GetComponent<Collider2D>());
		
	}
	
	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.CompareTag("Player")){
			gameObject.GetComponentInParent<Enemy>().playerSighted();
		}
		
	}
}
