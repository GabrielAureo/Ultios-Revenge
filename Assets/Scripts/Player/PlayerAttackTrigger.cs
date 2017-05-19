using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackTrigger : AttackTrigger {

    public ContactPoint2D contactPoint;

	override protected void OnTriggerEnter2D(Collider2D col){
		if (col.CompareTag("Enemy")) {
            col.transform.GetComponent<SquidDamage>().getCol(col, damage);
		}
	}
   
}
