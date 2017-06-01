using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Envy : Enemy {

    private bool charging = false;
    private bool laserShot = false;

    private Vector3 playerPos;

    public float timeForCharging;
    private float chargingTime;

    private bool right = true;

    private Animator anim2;
    private GameObject laser;

    private Vector2 fixedVelocity = new Vector2(1,0);

    override protected void Attack()
    {
        if(GameStory.reading){
            return;
        }
        anim2 = this.gameObject.transform.root.GetChild(2).gameObject.GetComponent<Animator>();
        laser = this.gameObject.transform.root.GetChild(2).GetChild(0).gameObject;

        rb.velocity = fixedVelocity * speed;
        if (!charging && Time.time > chargingTime && !laserShot)
        {
            anim2.SetInteger("shoting", 0);
            laser.SetActive(false);
            Charge();
        }
        else if(charging && Time.time > chargingTime && !laserShot){
            anim2.SetInteger("charging", 0);
            chargingTime = Time.time + 2.5f;
            charging = false;
            laserShot = true;
        }
        else if (Time.time > chargingTime && !charging && laserShot)
        {
            anim2.SetInteger("shoting", 1);
            laser.SetActive(true);
            chargingTime = Time.time + 2f;
            laserShot = false;
        }
    }

    public void Charge()
    {
        anim2.SetInteger("charging", 1);
        charging = true;
        chargingTime = Time.time + timeForCharging;
    }

    override protected void Patrol()
    {
        Attack();
    }

    protected override void Run()
    {
        Attack();
    }

    protected override void bum()
    {
        Attack();
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(fixedVelocity);
        if (col.CompareTag("Wall") && right){
            fixedVelocity = new Vector2(-1,0);
            right = false;
        }
        else if (col.CompareTag("Wall") && !right)
        {
            fixedVelocity = new Vector2(1,0);
            right = true;
        }
    }

}
