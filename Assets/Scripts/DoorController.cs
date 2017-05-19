using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour {

	public string roomToLoad;
	Collider2D trigger;

	Animator anim;

	// Use this for initialization
	void Start () {
		trigger = gameObject.GetComponent<Collider2D>();
		anim = gameObject.GetComponent<Animator>();
		trigger.enabled = false;

		anim.Play("Close Door");

		
	}

	void OnTriggerEnter2D(Collider2D col){
			if(col.gameObject.tag == "Player"){
				SceneManager.LoadScene(roomToLoad);
		}
	}

	public void openDoor(){
		trigger.enabled = true;
		anim.Play("Open Door");
	}
}
