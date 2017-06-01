using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {
	GameStory story;
	public string startRoomPath;
	public string endRoomPath;

	[SerializeField]
	int enemies;
	public List<DoorController> doors;
	public GameObject diary;
	List <string> Rooms = new List<string>{"First Room", "Second Room", "Third Room", "Fourth Room"};
	static string prevRoom;
	public string nextRoom;
	// Use this for initialization

	void Awake(){
		story = GameObject.FindGameObjectWithTag("Story").GetComponent<GameStory>();
		diary = GameObject.FindGameObjectWithTag("Diary");
		enemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
	}
	void Start () {
		if(diary != null){
			if(Diary.diaryRead){
				Diary.increaseDiary();
			}
			diary.SetActive(false);
		}

		foreach(GameObject door in GameObject.FindGameObjectsWithTag("Door")){
			DoorController doorscript = door.GetComponent<DoorController>();
			doorscript.closeDoor();
			doors.Add(doorscript);
		}
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
		openDoors();
		if(diary != null){
			revealDiary();
		}

	}


	void openDoors(){
		foreach(DoorController door in doors){
			if(string.IsNullOrEmpty(nextRoom)){
				nextRoom = pickARoom();
			}
			door.setRoom(nextRoom);
			door.openDoor();
		}
	}
	string pickARoom(){
		string rndRoom;
		List<string> validRooms = Rooms;

		Debug.Log(prevRoom);

		validRooms.Remove(prevRoom);
		int rndIndex = Random.Range(0,(validRooms.Count));

		prevRoom = validRooms[rndIndex];
		
		rndRoom = prevRoom;

		return rndRoom;
	}

	public void setDoors(string room){
		nextRoom = room;
		openDoors();
	}

	void revealDiary(){
		diary.SetActive(true);
	}

}