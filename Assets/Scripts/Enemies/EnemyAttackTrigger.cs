using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackTrigger : AttackTrigger {

    private GameObject playerDamage;
    private int value;

	override protected void OnTriggerEnter2D(Collider2D col){
        value = 0;
        playerDamage = GameObject.FindGameObjectWithTag("Player").transform.root.GetChild(1).gameObject;
        if (col.CompareTag("Player"))
        {
            foreach (Collider2D cols in playerDamage.GetComponents<Collider2D>())
            {
                value = value + 1;
                if (col.GetInstanceID() == cols.GetInstanceID()) { break; }
            }
            col.transform.root.GetComponent<IKillable>().takeDamage(damage, value);       
        }
    }

}
