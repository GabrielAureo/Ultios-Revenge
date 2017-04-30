using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {
	public GameStory story;
	int enemies;

	// Use this for initialization
	void Start () {
		enemies = GameObject.FindGameObjectsWithTag ("Enemy").Length;
	}
	
	// Update is called once per frame
	void Update () {
		//Mudar isso//
		if (enemies == 0) {
			if (!story.reading) {
				story.story.ChoosePathString ("clearedRoom");
				story.RefreshView ();
			}
		}
		
	}

	public void enemyKilled(){
		enemies--;
	}
}
