using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerKillable : MonoBehaviour, IKillable {
	public IEntity player;
	public GameObject gameover;
	// Use this for initialization
	void Start () {
		player = gameObject.GetComponent<IEntity>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void takeDamage(float damage){
		player.setHealth(damage);
		if(player.getHealth() <= 0){
			Die();
		}

	}

	public void Die(){
		Debug.Log("Você faleceu :c");
		/*GameObject.FindGameObjectWithTag("Game Over").SetActive(true);
		if(Input.GetKey(KeyCode.Space)){
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}*/

	}
}
