using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;
using TMPro;

public class GameStory : MonoBehaviour {
	public Story story;
	public TextAsset inkJSONAsset;

	public RectTransform textBox;
	public TextMeshProUGUI text;

	public bool reading{
		get{
			return textBox.gameObject.activeSelf;
		}
	}


	// Use this for initialization
	void Awake () {
		StartStory ();
		
	}
	void StartStory () {
		story = new Story (inkJSONAsset.text);
		RefreshView();
	}
		
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.E)){
			RefreshView();
		}
	}

	public void RefreshView(){
		if (story.canContinue) {
			textBox.gameObject.SetActive (true);
			text.text = story.Continue ().Trim();
		}else{
			HideView();
		}
	}

	void HideView(){
		textBox.gameObject.SetActive (false);
	}

}
