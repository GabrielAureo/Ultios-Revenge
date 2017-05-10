using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {
	
	private GameObject player;
	private float damage;

	// Use this for initialization
	void Start () {
		player = transform.root.gameObject;
		damage = player.GetComponent<PlayerController>().damage;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col){
		Debug.Log(col);
		if (col.CompareTag ("Enemy")) {
			col.transform.root.GetComponent<IKillable> ().takeDamage (damage);
		}
	}
}
