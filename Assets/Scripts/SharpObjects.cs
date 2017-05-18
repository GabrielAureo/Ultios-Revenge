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

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
