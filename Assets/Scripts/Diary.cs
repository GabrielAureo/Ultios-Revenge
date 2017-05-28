using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diary : Interactable {
	static int diaryIndex = 1;
	override public void Interact(){
		string diary = parseString(diaryIndex);
		storyPath = diary;
		if(diaryIndex < 3){
			diaryIndex = diaryIndex +1;
		}else{
			RoomManager roomManager = GameObject.FindGameObjectWithTag("Room Manager").GetComponent<RoomManager>();
			roomManager.setDoors("Boss Room");
		}

		if(!GameStory.reading){
			story.story.ChoosePathString(storyPath);
			story.RefreshView();
		}

		gameObject.SetActive(false);

	}

	string parseString(object o){
		Debug.Log("diary" + o.ToString());
		return "diary" + o.ToString();
	}
}
