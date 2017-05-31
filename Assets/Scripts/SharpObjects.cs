using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharpObjects : MonoBehaviour, IEntity {

    public float damage;

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
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
