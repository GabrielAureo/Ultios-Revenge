using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKillable : MonoBehaviour, IKillable {

	public Enemy enemy;

	public RoomManager room;

	// Use this for initialization
	void Start () {
		room = GameObject.FindGameObjectWithTag ("Room Manager").GetComponent<RoomManager> ();
		enemy = gameObject.GetComponent<Enemy>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Die(){
		room.enemyKilled ();
		gameObject.SetActive (false);

	}

	public void takeDamage(float damage){
		enemy.health -= damage;
		if (enemy.health <= 0) {
			Die ();
		}
	}
}
