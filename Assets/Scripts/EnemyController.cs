using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public int health;
	public int attack;
	public int damage;

	public RoomManager room;

	// Use this for initialization
	void Start () {
		room = GameObject.FindGameObjectWithTag ("Room Manager").GetComponent<RoomManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void Die(){
		room.enemyKilled ();
		gameObject.SetActive (false);

	}

	public void takeDamage(int damage){
		health -= damage;
		if (health <= 0) {
			Die ();
		}
	}
}
