using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour {

	Interactable targetInteractable;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(targetInteractable != null && Input.GetKeyDown(KeyCode.E)){
			targetInteractable.Interact();
		}
		
	}

	void OnTriggerEnter2D(Collider2D col){	
		Interactable root = col.transform.root.GetComponent<Interactable>();
		if(root != null){
			if (root.CompareTag("Interactable")){
				targetInteractable = root;
			}
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (targetInteractable == null){
			return;
		}
		Interactable root =  col.transform.root.GetComponent<Interactable>();
		if(targetInteractable == root){
			targetInteractable = null;
		}
	}
}
