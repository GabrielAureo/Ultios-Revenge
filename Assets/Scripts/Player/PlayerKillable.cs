using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerKillable : MonoBehaviour, IKillable {

    public IEntity player;
	public GameObject gameover;

    void Start () {
		player = gameObject.GetComponent<IEntity>();
	}
	
	void Update () {
		
	}

	public void takeDamage(float damage){
		player.setHealth(damage);
		if(player.getHealth() <= 0){
			Die();
		}
	}

	public void Die(){
        //Debug.Log("Você faleceu :c");
        SceneManager.LoadScene("Lose");
    }
}
