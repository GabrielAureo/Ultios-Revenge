using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntEnemy : Enemy {

    private bool charging = false;
    private bool running = false;
    private bool setPos = false;
    private Vector3 playerPos;

    public float timeForCharging;
    private float chargingTime;

    override protected void Attack()
    {
        rb.velocity = (player.transform.position - rb.transform.position).normalized * speed;
        anim.SetInteger("walking", 1);
    }

    override protected void Patrol()
    {
        rb.velocity = (player.transform.position - transform.position).normalized * 0;
        anim.SetInteger("walking", 0);
    }

    protected override void Run()
    {
        if (!running)
        {
            if (!charging)
            {
                Charge();
            }
        }
        else if(Time.time > chargingTime && running)
        {
            if (setPos)
            {
                playerPos = new Vector3(player.transform.position.x, player.transform.position.y, 0);
                anim.SetInteger("walking", 1);
                setPos = false;
            }
            Running(playerPos);
        }
    }

    public void Charge()
    {
        anim.SetInteger("walking", 0);
        charging = true;
        rb.velocity = (player.transform.position - transform.position).normalized * 0;
        chargingTime = Time.time + timeForCharging;
        charging = false;
        running = true;
        setPos = true;
    }

    public void Running(Vector3 playerPos)
    {
        if((float)transform.position.x != (float)playerPos.x && (float)transform.position.y != (float)playerPos.y)
        {
            rb.velocity = (playerPos - transform.position).normalized * speed * 5;
            if(transform.position.x > playerPos.x - 0.1 && transform.position.x < playerPos.x + 0.1 && transform.position.y > playerPos.y -0.1 && transform.position.y < playerPos.y + 0.1)
            {
                transform.position = playerPos;
            }
        }
        else
        {
            running = false;
        }
    }

    protected override void bum()
    {
        anim.SetBool("bum", true);
        rb.transform.root.transform.GetChild(0).GetComponent<CircleCollider2D>().radius = 4;
    }

}
