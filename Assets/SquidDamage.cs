﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidDamage : MonoBehaviour
{
    public GameObject monsterDamage;
    private int value;
    public AudioSource swordOnEnemy;
    public AudioClip slash;

    public void getCol(Collider2D col, float damage)
    {
        value = 0;  
        foreach (Collider2D cols in monsterDamage.GetComponents<Collider2D>())
        {
            value = value + 1;
            if (col.GetInstanceID() == cols.GetInstanceID()) { break; }
        }
        swordOnEnemy = GetComponent<AudioSource>();
        swordOnEnemy.PlayOneShot(slash, 10);
        col.transform.root.GetComponent<IKillable>().takeDamage(damage, value);
    }   
}
