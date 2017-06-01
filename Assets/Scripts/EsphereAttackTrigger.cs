using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsphereAttackTrigger : MonoBehaviour {

    protected GameObject entity;
    protected float damage;
    private GameObject playerDamage;
    private int value;

    // Use this for initialization
    void Start()
    {
        entity = transform.root.gameObject;
        damage = entity.GetComponent<IEntity>().getDamage();
        Debug.Log(damage);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
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
