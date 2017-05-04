using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {
	public GameStory story;
	public string startRoomPath;
	public string endRoomPath;
	[SerializeField]
	int enemies;

	public GameObject diary;

	// Use this for initialization
	void Start () {
		diary.SetActive(false);
		enemies = GameObject.FindGameObjectsWithTag ("Enemy").Length;
		if(startRoomPath != ""){
			if (!story.reading) {
				story.story.ChoosePathString (startRoomPath);
				story.RefreshView ();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void enemyKilled(){
		enemies--;
		if(enemies == 0){
			clearedRoomDialog();
			diary.SetActive(true);
		}
	}

	void clearedRoomDialog(){
		if(endRoomPath != ""){
			if (!story.reading) {
				story.story.ChoosePathString (endRoomPath);
				story.RefreshView ();
			}
		}
	}
}
