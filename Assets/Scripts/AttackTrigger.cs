using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackTrigger : MonoBehaviour {
	
	protected GameObject entity;
	protected float damage;

	// Use this for initialization
	void Start () {
		entity = transform.root.gameObject;
		damage = entity.GetComponent<IEntity>().getDamage();
	}

	protected abstract void OnTriggerEnter2D(Collider2D col);
}
