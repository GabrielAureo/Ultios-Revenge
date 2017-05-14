using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour, IEntity {
	private Rigidbody2D rb;
	private GameObject atkPivot;

	public float health;
	public float damage;
	public float speed;
	
	public float atkCooldown;

	public float atkDuration;

	private float nextAtk;



	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D>();
		atkPivot = transform.GetChild(0).gameObject;
		atkPivot.SetActive (false);
	}

	void Update(){
		if (GameStory.reading) {
			rb.velocity = Vector2.zero;
			return;
		}

		Movement();
		Combat();
		
	}

	void Movement(){
		rb.velocity = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical")) * speed;
	}

	void Combat(){
		if(Input.GetAxisRaw("Horizontal") > 0){
			atkPivot.transform.eulerAngles = new Vector3(0,0,270);
		}else if(Input.GetAxisRaw("Horizontal") < 0){
			atkPivot.transform.eulerAngles = new Vector3(0,0,90);
		}else if(Input.GetAxisRaw("Vertical") > 0){
			atkPivot.transform.eulerAngles = new Vector3(0,0,0);
		}else if(Input.GetAxisRaw("Vertical") < 0){
			atkPivot.transform.eulerAngles = new Vector3(0,0,180);
		}

		if (Time.time > nextAtk && Input.GetButton("Fire1")) {
			nextAtk = Time.time + atkCooldown;
			StartCoroutine(Attack());
		}
	}

	IEnumerator Attack(){
		atkPivot.SetActive(true);
		yield return new WaitForSeconds(atkDuration);
		atkPivot.SetActive(false);
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
