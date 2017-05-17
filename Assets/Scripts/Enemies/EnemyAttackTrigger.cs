using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackTrigger : AttackTrigger {

	override protected void OnTriggerEnter2D(Collider2D col){
		if (col.CompareTag("Player")) {
			col.transform.root.GetComponent<IKillable>().takeDamage (damage);
		}
	}

}
