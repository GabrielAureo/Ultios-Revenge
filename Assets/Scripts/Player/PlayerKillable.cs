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
        player.setKnockingBack();
        if (findDamage == 1)
        {
            player.setThrowing(1);
            yield return new WaitForSeconds(damageTime);
        }
        else if (findDamage == 2)
        {
            player.setThrowing(2);
            yield return new WaitForSeconds(damageTime);
        }
        else if (findDamage == 3)
        {
            player.setThrowing(3);
            yield return new WaitForSeconds(damageTime);
        }
        else if (findDamage == 4)
        {
            player.setThrowing(4);
            yield return new WaitForSeconds(damageTime);
        }
        player.setKnockingBack();
        pushingBack = false;
    }

	public void Die(){
        Debug.Log("Morreu");
        GameObject.FindGameObjectWithTag("Game Over").GetComponent<GameOver>().enable();
    }
}
