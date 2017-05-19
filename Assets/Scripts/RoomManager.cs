using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {
	public GameStory story;
	public string startRoomPath;
	public string endRoomPath;
	[SerializeField]
	int enemies;

	public List<DoorController> doors;

	public GameObject diary;

	// Use this for initialization
	void Start () {
		story = GameObject.FindGameObjectWithTag("Story").GetComponent<GameStory>();

		if(diary != null){
			diary.SetActive(false);
		}

		foreach(GameObject door in GameObject.FindGameObjectsWithTag("Door")){
			doors.Add(door.GetComponent<DoorController>());
		}

		enemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

		if(startRoomPath != ""){
			if (!GameStory.reading) {
				story.story.ChoosePathString (startRoomPath);
				story.RefreshView ();
			}
		}

		if(enemies == 0){
			clearedRoom();
		}
	}
	
	// Update is called once per frame
	void Update () {
    }

	public void enemyKilled(){
        Debug.Log("Enemy Killed");
        enemies--;
		if(enemies == 0){
			clearedRoom();
			if(diary != null)
				diary.SetActive(true);
		}
	}

	void clearedRoom(){
		if(endRoomPath != ""){
			if (!GameStory.reading) {
				story.story.ChoosePathString (endRoomPath);
				story.RefreshView ();
			}
		}
		foreach(DoorController door in doors){
			door.openDoor();
		}
	}
}
