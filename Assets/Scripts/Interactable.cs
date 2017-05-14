using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class Interactable : MonoBehaviour {

	public string storyPath;
	public GameStory story;

	public void Interact(){
		if(!GameStory.reading){
			story.story.ChoosePathString(storyPath);
			story.RefreshView();
		}
	}

}
