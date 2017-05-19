using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightController : MonoBehaviour {

	private Collider2D radius;
    private bool shouldAttack = false;

	// Use this for initialization
	void Start () {
		radius = gameObject.GetComponent<CircleCollider2D>();
		Physics2D.IgnoreCollision (radius, transform.root.GetComponent<Collider2D>());
		Physics2D.IgnoreCollision (radius, transform.root.FindChild("Hit Trigger").GetComponent<Collider2D>());
	}

    void Update()
    {
        if(shouldAttack)
        {
            gameObject.GetComponentInParent<Enemy>().playerSighted();
        }else
        {
            gameObject.GetComponentInParent<Enemy>().playerSightedStop();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            shouldAttack = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            shouldAttack = false;
        }
    }

}
