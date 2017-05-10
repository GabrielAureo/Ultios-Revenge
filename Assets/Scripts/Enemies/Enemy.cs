using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {

	public Rigidbody2D rb;
	public GameObject player;
	public float health;
	public float attack;
	public GameStory story;
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

		if(!story.reading){
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
}
