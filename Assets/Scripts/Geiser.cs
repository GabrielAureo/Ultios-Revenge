using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geiser : MonoBehaviour, IEntity {

    public float damage;
    public float disableTime;

    private float burning;
    private float stop;

    private bool active;

    private Animator anim;
    private GameObject trig;

    public float getDamage()
    {
        return damage;
    }

    public float getHealth()
    {
        return 0;
    }

    public void setHealth(float damage)
    {
        //
    }

    public void setKnockingBack()
    {
        //
    }
    public void setThrowing(int i)
    {
        //
    }

    // Use this for initialization
    void Start()
    {
        active = false;
        stop = disableTime + Time.time;
        anim = gameObject.transform.root.GetComponent<Animator>();
        trig = gameObject.transform.root.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > stop && active)
        {
            anim.SetBool("act", false);
            trig.SetActive(false);
            stop = Time.time + disableTime;
            active = false;
        }
        else if(Time.time > stop && !active)
        {
            anim.SetBool("act", true);
            trig.SetActive(true);
            stop = Time.time + 1;
            active = true;
        }
    }
}
