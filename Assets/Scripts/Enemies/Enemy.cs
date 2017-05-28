using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IEntity {

	protected Rigidbody2D rb;
	protected GameObject player;
	protected GameStory story;

    private SpriteRenderer sprite;

    public float health;
	public float damage;
	public float speed;
	private bool shouldPatrol = true;
    private bool shouldRun = false;

    public float damageSpriteTime;
    private float backToDefault;

    // Use this for initialization
    void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
        sprite = this.gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
		story = GameObject.FindGameObjectWithTag("Story").GetComponent<GameStory>();
	}
	
	// Update is called once per frame
	void Update () {
        if(!GameStory.reading || !GameOver.gameOver){
			if(shouldPatrol){
				Patrol();
			}else{
                if (!shouldRun)
                {
                    Attack();
                }else
                {
                    Run();
                }
            }	
		}
        SpriteRend();
	}

	protected abstract void Patrol ();

	protected abstract void Attack ();

    protected abstract void Run();

    public void playerSighted(){
		shouldPatrol = false;
	}

    public void playerSightedStop()
    {
        shouldPatrol = true;
    }

    public float getHealth(){
		return health;
	}

	public float getDamage(){
		return damage;
	}

    void SpriteRend()
    {
        if (Time.time > backToDefault)
        {
            sprite.color = Color.white;
        }
    }

    public void setHealth(float damage){
        shouldRun = true;
		health -= damage;
        sprite.color = Color.red;
        backToDefault = Time.time + damageSpriteTime;
    }

}
