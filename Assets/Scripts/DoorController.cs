using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour {

	[SerializeField]
	string roomToLoad;
	[SerializeField]
	Collider2D trigger;
	[SerializeField]
	Animator anim;

	// Use this for initialization
	void Awake () {
		trigger = gameObject.GetComponent<Collider2D>();
		anim = gameObject.GetComponent<Animator>();

	}

	public void closeDoor(){
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

	public void setRoom(string room){
		roomToLoad = room;
	}
}
