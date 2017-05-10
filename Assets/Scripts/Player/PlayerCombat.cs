using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour {

	public GameObject atkTrigger;

	public float atkCooldown;

	public float atkDuration;

	private float nextAtk;

	KeyCode[] keys = {KeyCode.UpArrow, KeyCode.LeftArrow,KeyCode.DownArrow,KeyCode.RightArrow};

	 public KeyCode AnyKey(KeyCode[] aKeys){
		foreach(KeyCode K in aKeys)
			if (Input.GetKey(K))
				return K;
		return KeyCode.None;
 	}
	void Start () {
		atkTrigger = transform.GetChild(0).gameObject;
		atkTrigger.SetActive (false);
		
	}
	
	// Update is called once per frame
	void Update () {
		KeyCode pressed = AnyKey(keys);
		if(pressed != KeyCode.None){
			rotatePivot(pressed);
		}
	}
		

	void rotatePivot(KeyCode key){
		switch(key){
			case(KeyCode.UpArrow):
				transform.eulerAngles = new Vector3(0,0,0);
				break;
			case(KeyCode.LeftArrow):
				transform.eulerAngles = new Vector3(0,0,90);
				break;
			case(KeyCode.DownArrow):
				transform.eulerAngles = new Vector3(0,0,180);
				break;
			case(KeyCode.RightArrow):
				transform.eulerAngles = new Vector3(0,0,270);
				break;
		}

		if (Time.time > nextAtk) {
			nextAtk = Time.time + atkCooldown;
			StartCoroutine(Attack());
		}
	}

	IEnumerator Attack(){
		atkTrigger.SetActive(true);
		yield return new WaitForSeconds(atkDuration);
		atkTrigger.SetActive(false);
	}


}
