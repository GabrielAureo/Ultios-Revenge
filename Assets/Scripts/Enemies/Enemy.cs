using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IEntity {

	protected Rigidbody2D rb;
	protected GameObject player;
	protected GameStory story;
    protected Animator anim;

    private SpriteRenderer sprite;

    public float health;
	public float damage;
	public float speed;
	private bool shouldPatrol = true;
    private bool shouldRun = false;

    private float xPos;
    private bool isToFlip = false;

    public float damageSpriteTime;
    private float backToDefault;

    // Use this for initialization
    void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
        sprite = this.gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
		story = GameObject.FindGameObjectWithTag("Story").GetComponent<GameStory>();
        anim = this.gameObject.GetComponent<Animator>();
        xPos = rb.transform.position.x;
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
        if(xPos < rb.transform.position.x && !isToFlip)
        {
            isToFlip = true;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if(xPos > rb.transform.position.x && isToFlip)
        {
            isToFlip = false;
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        xPos = rb.transform.position.x;
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

    public void setKnockingBack()
    {

    }

    public void setThrowing(int i)
    {

    }

    void SpriteRend()
    {
        if (Time.time > backToDefault)
        {
            sprite.color = Color.white;
        }
    }

    public void setHealth(float damage){
        bum();
        shouldRun = true;
		health -= damage;
        sprite.color = Color.red;
        backToDefault = Time.time + damageSpriteTime;
    }

    protected abstract void bum();

}
