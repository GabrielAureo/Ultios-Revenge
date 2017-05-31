using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKillable : MonoBehaviour, IKillable {

	public IEntity enemy;
	public RoomManager room;
    public Rigidbody2D rb;
    private GameObject playerPivot;

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
        playerPivot = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject;
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
        Debug.Log("Quied" + playerPivot.transform.rotation.z);
        pushingBack = true;
        if (playerPivot.transform.eulerAngles.z == 270)
        {
            Vector3 newPosition = new Vector3(rb.transform.position.x + 0.8f, rb.transform.position.y, rb.transform.position.z);
            yield return rb.transform.position = newPosition;
        }
        else if (playerPivot.transform.eulerAngles.z == 0)
        {
            Vector3 newPosition = new Vector3(rb.transform.position.x, rb.transform.position.y + 0.8f, rb.transform.position.z);
            yield return rb.transform.position = newPosition;
        }
        else if (playerPivot.transform.eulerAngles.z == 90)
        {
            Vector3 newPosition = new Vector3(rb.transform.position.x - 0.8f, rb.transform.position.y, rb.transform.position.z);
            yield return rb.transform.position = newPosition;
        }
        else if (playerPivot.transform.eulerAngles.z == 180)
        {
            Vector3 newPosition = new Vector3(rb.transform.position.x, rb.transform.position.y - 0.8f, rb.transform.position.z);
            yield return rb.transform.position = newPosition;
        }
        pushingBack = false;
    }
}
