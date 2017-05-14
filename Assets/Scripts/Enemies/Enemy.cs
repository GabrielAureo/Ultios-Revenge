using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IEntity {

	protected Rigidbody2D rb;
	protected GameObject player;
	protected GameStory story;

	public float health;
	public float damage;
	public float speed;
	private bool shouldPatrol = true;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		rb = gameObject.GetComponent<Rigidbody2D>();
		story = GameObject.FindGameObjectWithTag("Story").GetComponent<GameStory>();
	}
	
	// Update is called once per frame
	void Update () {

		if(!GameStory.reading){
			if(shouldPatrol){
				Patrol();
			}else{
				Attack();
			}	
		}		
	}

	protected abstract void Patrol ();

	protected abstract void Attack ();

	public void playerSighted(){
		shouldPatrol = false;
	}

	public float getHealth(){
		return health;
	}

	public float getDamage(){
		return damage;
	}

	public void setHealth(float damage){
		health -= damage;
	}

}
