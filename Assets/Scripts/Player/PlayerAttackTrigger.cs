using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackTrigger : AttackTrigger {

    public ContactPoint2D contactPoint;
    public AudioSource swordOnEnemy;
    public AudioClip slash;

    override protected void OnTriggerEnter2D(Collider2D col){
		if (col.CompareTag("Enemy")) {
            //swordOnEnemy = GetComponent<AudioSource>();
            //swordOnEnemy.PlayOneShot(slash, 10);
            col.transform.GetComponent<SquidDamage>().getCol(col, damage);
		}
	}
   
}
