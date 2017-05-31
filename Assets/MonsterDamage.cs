using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDamage : MonoBehaviour
{
    public GameObject monsterDamage;
    private int value;
    public AudioSource swordOnEnemy;
    public AudioClip slash;

    public void getCol(Collider2D col, float damage)
    {
        value = 1;
        swordOnEnemy = GetComponent<AudioSource>();
        swordOnEnemy.PlayOneShot(slash, 10);
        col.transform.root.GetComponent<IKillable>().takeDamage(damage, value);
    }   
}
