using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour {

	public GameObject atkPivot;

	public float atkCooldown;

	public float atkDuration;

	private float nextAtk;

	void Start () {
		atkPivot = transform.GetChild(0).gameObject;
		atkPivot.SetActive (false);	
	}
	
	// Update is called once per frame
	void Update () {
		Combat();
	}
		

	void Combat(){
		if(Input.GetAxisRaw("Horizontal") > 0){
			atkPivot.transform.eulerAngles = new Vector3(0,0,270);
		}else if(Input.GetAxisRaw("Horizontal") < 0){
			atkPivot.transform.eulerAngles = new Vector3(0,0,90);
		}else if(Input.GetAxisRaw("Vertical") > 0){
			atkPivot.transform.eulerAngles = new Vector3(0,0,0);
		}else if(Input.GetAxisRaw("Vertical") < 0){
			atkPivot.transform.eulerAngles = new Vector3(0,0,180);
		}

		if (Time.time > nextAtk && Input.GetButton("Fire1")) {
			nextAtk = Time.time + atkCooldown;
			StartCoroutine(Attack());
		}
	}

	IEnumerator Attack(){
		atkPivot.SetActive(true);
		yield return new WaitForSeconds(atkDuration);
		atkPivot.SetActive(false);
	}


}
