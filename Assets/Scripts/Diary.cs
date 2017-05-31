using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diary : Interactable {
	static int diaryIndex = 1;
	public static bool diaryRead = false;

	public Sprite[] diaryImages;

	void Start(){
		SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = diaryImages[diaryIndex -1];
	}
	override public void Interact(){
		string diary = parseString(diaryIndex);
		storyPath = diary;

		diaryRead = true;
		/*if(diaryIndex < 3){
			diaryIndex = diaryIndex +1;
		}else{
			RoomManager roomManager = GameObject.FindGameObjectWithTag("Room Manager").GetComponent<RoomManager>();
			roomManager.setDoors("Boss Room");
		}*/

		if (diaryIndex == 3){
			RoomManager roomManager = GameObject.FindGameObjectWithTag("Room Manager").GetComponent<RoomManager>();
			roomManager.setDoors("Boss Room");
		}

		if(!GameStory.reading){
			story.story.ChoosePathString(storyPath);
			story.RefreshView();
		}

		//gameObject.SetActive(false);

	}

	string parseString(object o){
		Debug.Log("diary" + o.ToString());
		return "diary" + o.ToString();
	}

	public static void increaseDiary(){
		if(diaryIndex < 3){
			diaryIndex = diaryIndex +1;
		}
		diaryRead = false;
		/*else{
			RoomManager roomManager = GameObject.FindGameObjectWithTag("Room Manager").GetComponent<RoomManager>();
			roomManager.setDoors("Boss Room");
		}*/
	}
}
