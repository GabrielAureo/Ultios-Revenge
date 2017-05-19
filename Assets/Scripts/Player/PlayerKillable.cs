using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerKillable : MonoBehaviour, IKillable {

    public IEntity player;
    public Rigidbody2D rb;
	public GameObject gameover;

    private bool pushingBack = false;

    private float inDamage;
    public float damageTime;

    void Start () {
		player = gameObject.GetComponent<IEntity>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        inDamage = 0;
	}
	
	void Update () {
		
	}

	public void takeDamage(float damage, int findDamage){
        if (!pushingBack && Time.time > inDamage)
        {
            player.setHealth(damage);
            StartCoroutine(ThrowBack(findDamage));
            inDamage = Time.time + damageTime;
        }
        if (player.getHealth() <= 0)
        {
            Die();
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
        else if (findDamage == 2)
        {
            Vector3 newPosition = new Vector3(rb.transform.position.x, rb.transform.position.y + 0.8f, rb.transform.position.z);
            yield return rb.transform.position = newPosition;
        }
        else if (findDamage == 3)
        {
            Vector3 newPosition = new Vector3(rb.transform.position.x - 0.8f, rb.transform.position.y, rb.transform.position.z);
            yield return rb.transform.position = newPosition;
        }
        else if (findDamage == 4)
        {
            Vector3 newPosition = new Vector3(rb.transform.position.x, rb.transform.position.y - 0.8f, rb.transform.position.z);
            yield return rb.transform.position = newPosition;
        }
        pushingBack = false;
    }

	public void Die(){
        SceneManager.LoadScene("Lose");
    }
}
