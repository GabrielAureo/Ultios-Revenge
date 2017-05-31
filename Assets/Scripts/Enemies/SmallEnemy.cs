using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEnemy : Enemy {

    public GameObject fireball;
    public Rigidbody2D fire;
    public float fireBallSpeed;
    private Boolean isShoting = false;

    override protected void Attack(){
        rb.velocity = (player.transform.position - rb.transform.position).normalized * speed;
	}

	override protected void Patrol(){
        rb.velocity = (player.transform.position - transform.position).normalized * 0;
    }

    protected override void Run()
    {
        rb.velocity = (rb.transform.position - player.transform.position).normalized * speed;
        if (!isShoting)
        {
            StartCoroutine(Shoting());
        }
    }

    protected override void bum()
    {
     //  
    }

    IEnumerator Shoting()
    {
        isShoting = true;
        fire = fireball.GetComponent<Rigidbody2D>();
        Rigidbody2D fireClone;
        fireClone = Instantiate(fire, transform.position, transform.rotation) as Rigidbody2D;
        fireClone.velocity = (player.transform.position - rb.transform.position).normalized * fireBallSpeed;
        Debug.Log(fireClone.velocity);
        yield return new WaitForSeconds(2f);
        isShoting = false;
    }


}
