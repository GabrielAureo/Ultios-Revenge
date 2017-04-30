using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {
	
	private GameObject player;

	// Use this for initialization
	void Start () {
		player = transform.parent.gameObject;
		Physics2D.IgnoreCollision (GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.CompareTag ("Enemy")) {
			col.gameObject.GetComponent<EnemyController> ().takeDamage (1);
		}
	}
}
