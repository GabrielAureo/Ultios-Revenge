using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackTrigger : AttackTrigger {

	override protected void OnTriggerEnter2D(Collider2D col){
		if (col.CompareTag ("Enemy")) {
			col.transform.root.GetComponent<IKillable> ().takeDamage (damage);
		}
	}
}
