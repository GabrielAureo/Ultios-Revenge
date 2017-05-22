using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : AttackTrigger
{
    public GameObject playerDamage;
    private GameObject rb;
    private int value;

    override protected void OnTriggerEnter2D(Collider2D col)
    {
        value = 0;
        if (col.CompareTag("Player"))
        {
            foreach (Collider2D cols in playerDamage.GetComponents<Collider2D>())
            {
                value = value + 1;
                if (col.GetInstanceID() == cols.GetInstanceID()) { break; }
            }
            col.transform.root.GetComponent<IKillable>().takeDamage(damage, value);
            rb = this.transform.root.gameObject;
            Destroy(rb);
        }
        else if (col.CompareTag("Block"))
        {
            rb = this.transform.root.gameObject;
            Destroy(rb);
        }
    }
}

