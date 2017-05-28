using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class Interactable : MonoBehaviour {

	public string storyPath;
	public GameStory story;
	void Awake(){
		story = GameObject.FindGameObjectWithTag("Story").GetComponent<GameStory>();
	}

	public virtual void Interact(){
		if(!GameStory.reading){
			story.story.ChoosePathString(storyPath);
			story.RefreshView();
		}
	}

}
