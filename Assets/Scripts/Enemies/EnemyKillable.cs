using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKillable : MonoBehaviour, IKillable {

	public IEntity enemy;
	public RoomManager room;
    public Rigidbody2D rb;

    public bool pushingBack;

    private float inDamage;
    public float damageTime;

    // Use this for initialization
    void Start()
    {
        room = GameObject.FindGameObjectWithTag("Room Manager").GetComponent<RoomManager>();
        enemy = gameObject.GetComponent<IEntity>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        inDamage = 0;
    }

    public void takeDamage(float damage, int i)
    {
        if (Time.time > inDamage && !pushingBack)
        {
            enemy.setHealth(damage);
            StartCoroutine(ThrowBack(i));
            inDamage = Time.time + damageTime;
        }
        if (enemy.getHealth() <= 0)
        {
            Die();
        }
    }

    // Update is called once per frame
    void Update () {
		
	}


	public void Die(){
        if (gameObject.activeSelf)
        {
            room.enemyKilled();
            gameObject.SetActive(false);
        }
	}

    IEnumerator ThrowBack(int findDamage)
    {
        pushingBack = true;
        if (findDamage == 1)
        {
            Vector3 newPosition = new Vector3(rb.transform.position.x + 0.8f, rb.transform.position.y, rb.transform.position.z);
            yield return rb.transform.position = newPosition;
        }
        else if (findDamage == 4)
        {
            Vector3 newPosition = new Vector3(rb.transform.position.x, rb.transform.position.y + 0.8f, rb.transform.position.z);
            yield return rb.transform.position = newPosition;
        }
        else if (findDamage == 2)
        {
            Vector3 newPosition = new Vector3(rb.transform.position.x - 0.8f, rb.transform.position.y, rb.transform.position.z);
            yield return rb.transform.position = newPosition;
        }
        else if (findDamage == 3)
        {
            Vector3 newPosition = new Vector3(rb.transform.position.x, rb.transform.position.y - 0.8f, rb.transform.position.z);
            yield return rb.transform.position = newPosition;
        }
        pushingBack = false;
    }
}
