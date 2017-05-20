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
	public TextMeshProUGUI currentSpeaker;
	public TextMeshProUGUI dialog;
	public Sprite dialogBG;
	public Sprite bookBG;

	Color dialogColor = new Color32 (255,255,255,255);
	Color bookColor = new Color32 (142,96,4,255);
	[SerializeField]
	Color currentColor;

	public static bool reading;


	// Use this for initialization
	void Awake () {
		StartStory ();
		
	}
	void StartStory () {
		story = new Story (inkJSONAsset.text);
		//RefreshView();
	}
		
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1") && reading){
			RefreshView();
		}
	}

	public void RefreshView(){
		if (story.canContinue) {
			textBox.gameObject.SetActive (true);
			reading = true;
			string rawtext = story.Continue ().Trim();
			dialog.text = ParseContent(rawtext);
			dialog.color = currentColor;
			

		}else{
			HideView();
		}
	}

	public string ParseContent (string rawContent) {
		string subjectID = "";
		string content = "";
		if(!TrySplitContentBySearchString(rawContent, ": ", ref subjectID, ref content)) return rawContent;
		ChangeSpeaker(subjectID);
		return content;
	}

	public bool TrySplitContentBySearchString (string rawContent, string searchString, ref string left, ref string right) {
		int firstSpecialCharacterIndex = rawContent.IndexOf(searchString);
		if(firstSpecialCharacterIndex == -1) return false;
		
		left = rawContent.Substring(0, firstSpecialCharacterIndex).Trim();
		right = rawContent.Substring(firstSpecialCharacterIndex+searchString.Length, rawContent.Length-firstSpecialCharacterIndex-searchString.Length).Trim();
		return true;
	}

	void ChangeSpeaker(string speaker){
		if(speaker != "Diary"){
			currentSpeaker.text = speaker;
			textBox.GetComponent<Image>().sprite = dialogBG;
			currentColor = dialogColor;
		} else {
			currentSpeaker.text = "";
			textBox.GetComponent<Image>().sprite = bookBG;
			currentColor = bookColor;
		}
	}
	void HideView(){
		textBox.gameObject.SetActive (false);
		reading = false;
	}

}
