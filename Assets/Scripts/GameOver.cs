using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	public static bool gameOver = false;
	Animator anim;

	void Awake(){
		anim = gameObject.GetComponent<Animator>();
	}
	void Start () {
		DontDestroyOnLoad(gameObject);
		disable();	
	}
	
	// Update is called once per frame
	void Update () {
		if(gameOver && Input.GetKeyDown(KeyCode.R)){
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			disable();
		}
	}


	void disable(){
		foreach(Transform child in transform){
			child.gameObject.SetActive(false);
		}
		gameOver = false;
	}


	public void enable(){
		foreach(Transform child in transform){
			child.gameObject.SetActive(true);
		}
		gameOver = true;
	}


}
