using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEnemy : Enemy {

	override protected void Attack(){
		rb.velocity = (player.transform.position - transform.position).normalized * speed;
	}

	override protected void Patrol(){
        rb.velocity = (player.transform.position - transform.position).normalized * 0;
    }


}
