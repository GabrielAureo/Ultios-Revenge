using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKillable : MonoBehaviour, IKillable {

	public IEntity enemy;
	public RoomManager room;

	// Use this for initialization
	void Start () {
		room = GameObject.FindGameObjectWithTag ("Room Manager").GetComponent<RoomManager> ();
		enemy = gameObject.GetComponent<IEntity>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Die(){
		room.enemyKilled ();
		gameObject.SetActive (false);

	}

	public void takeDamage(float damage){
		enemy.setHealth(damage);
		if (enemy.getHealth() <= 0) {
			Die ();
		}
	}
}
