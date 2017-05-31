using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player: MonoBehaviour, IEntity {

    private Rigidbody2D rb;
	private GameObject atkPivot;
    private GameObject blockTrig;
    private GameObject atkTrig;
    private SpriteRenderer sprite;
    private Image life;

	public float health;
	public float damage;
	public float speed;

    public float atkSpriteCount;

    public float nextAtkBotSprite;
    public float nextAtkUpSprite;
    public float nextAtkRightSprite;
    public float nextAtkLeftSprite;

    public float atkCooldown;

	public float atkDuration;

	private float nextAtk;

    private bool dashing;

    public Animator animator;

    private bool setDamage;
    public float damageSpriteTime;
    private float backToDefault;
    private float dashingTime;
    private float dashingValue;
    private float speedFixed;
    private bool blocking;

    private bool knockingBack;
    private int throwingInteger;
    public int pushingDistance;

    private int moving;
    private way state;
    enum way {left, right, bottom, upper, stop };

    // Use this for initialization
    void Start () {
		rb = this.gameObject.GetComponent<Rigidbody2D>();
        sprite = this.gameObject.GetComponent<SpriteRenderer>();
		atkPivot = transform.GetChild(0).gameObject;
        atkTrig = transform.GetChild(0).GetChild(0).gameObject;
        blockTrig = transform.GetChild(0).GetChild(1).gameObject;
        life = GameObject.FindGameObjectWithTag("Canvas").transform.root.GetChild(1).gameObject.GetComponent<Image>();
        atkPivot.SetActive(false);
        animator = GetComponent<Animator>();
        animator.SetFloat("blockway", 5f);
        moving = 0;
        state = way.stop;
    }

    void Update(){
		if (GameStory.reading || GameOver.gameOver) {
			rb.velocity = Vector2.zero;
			return;
		}

        SpriteRend();
        whichWay();
        Animation();
        Movement();
		Combat();
        if (atkTrig.activeSelf == false)
        {
            Blocked();
        }
        //speedDecressing();
    }

    void Blocked()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            atkPivot.SetActive(true);
            blockTrig.SetActive(true);
            animator.SetFloat("block", 1f);
            animator.SetFloat("walk", 0f);
            blocking = true;
        }
        else if(Input.GetKeyUp(KeyCode.Mouse1)) {
            blockTrig.SetActive(false);
            atkPivot.SetActive(false);
            animator.SetFloat("block", -1f);
            blocking = false;
        }
    }

    void whichWay()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            state = way.upper;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            state = way.bottom;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            state = way.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            state = way.right;
        }
        if(Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0 && !Input.anyKey)
        {
            state = way.stop;
        }

        if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            moving = 1;
        }

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            moving = 2;
        }

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            moving = 3;
        }

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            moving = 4;
        }

        if (moving == 1)
        {
            if (Input.GetKeyUp(KeyCode.W))
            {
                state = way.right;
                moving = 0;
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                state = way.upper;
                moving = 0;
            }
        }

        if (moving == 2)
        {
            if (Input.GetKeyUp(KeyCode.W))
            {
                state = way.left;
                moving = 0;
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                state = way.upper;
                moving = 0;
            }
        }

        if (moving == 3)
        {
            if (Input.GetKeyUp(KeyCode.S))
            {
                state = way.right;
                moving = 0;
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                state = way.bottom;
                moving = 0;
            }
        }

        if (moving == 4)
        {
            if (Input.GetKeyUp(KeyCode.S))
            {
                state = way.left;
                moving = 0;
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                state = way.bottom;
                moving = 0;
            }
        }
    }

    void Animation()
    {
     
        animator.SetFloat("attackBot", (nextAtkBotSprite - Time.time));
        animator.SetFloat("attackUp", (nextAtkUpSprite - Time.time));
        animator.SetFloat("attackRight", (nextAtkRightSprite - Time.time));
        animator.SetFloat("attackLeft", (nextAtkLeftSprite - Time.time));
        animator.SetBool("downMovement", false);

        if (Input.GetAxisRaw("Vertical") > 0 && state == way.upper)
        {
            animator.SetFloat("stay", 0f);
            animator.SetFloat("walk", 1f);
        }
        if (Input.GetAxisRaw("Horizontal") < 0 && state == way.left)
        {
            animator.SetFloat("stay", 0f);
            animator.SetFloat("walk", 7f);
        }
        if (Input.GetAxisRaw("Vertical") < 0 && state == way.bottom)
        {
            animator.SetFloat("stay", 0f);
            animator.SetFloat("walk", 5f);
        }
        if (Input.GetAxisRaw("Horizontal") > 0 && state == way.right)
        {
            animator.SetFloat("stay", 0f);
            animator.SetFloat("walk", 3f);
        }
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0 && state == way.stop)
        {
            animator.SetFloat("stay", 2f);
            animator.SetFloat("walk", 0f);
        }
    }

	void Movement(){
        if (!blocking)
        {
            if (!knockingBack)
            {
                if (Input.GetKeyDown(KeyCode.Mouse2))
                {
                    StartCoroutine(Dash());
                }
                else
                {
                    rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed;
                }
            }
            else if (knockingBack)
            {
                if (throwingInteger == 1)
                {
                    rb.velocity = new Vector2(pushingDistance, 0) * speed;
                }
                else if (throwingInteger == 2)
                {
                    rb.velocity = new Vector2(0, pushingDistance) * speed;
                }
                else if (throwingInteger == 3)
                {
                    rb.velocity = new Vector2(-pushingDistance, 0) * speed;
                }
                else if (throwingInteger == 4)
                {
                    rb.velocity = new Vector2(0, -pushingDistance) * speed;
                }
            }
        }
        else if (blocking)
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * 0;
        }
    }

	void Combat(){

		if(animator.GetCurrentAnimatorStateInfo(0).IsName("walk_right"))
        {
			atkPivot.transform.eulerAngles = new Vector3(0,0,270);
            animator.SetFloat("blockway", 3f);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("walk_left"))
        {
			atkPivot.transform.eulerAngles = new Vector3(0,0,90);
            animator.SetFloat("blockway", 1f);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("walk_upper"))
        {
			atkPivot.transform.eulerAngles = new Vector3(0,0,0);
            animator.SetFloat("blockway", 5f);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("walk_bottom"))
        {
			atkPivot.transform.eulerAngles = new Vector3(0,0,180);
            animator.SetFloat("blockway", 7f);
        }


        if (Time.time > nextAtk && Input.GetButton("Fire1")) {
            nextAtk = Time.time + atkCooldown;
			StartCoroutine(Attack());
            if (Time.time > atkSpriteCount && atkPivot.transform.eulerAngles.z == 180)
            {
                nextAtkBotSprite = Time.time + atkSpriteCount;
                animator.SetFloat("attackBot", nextAtkBotSprite);
            }
            else if (Time.time > atkSpriteCount && atkPivot.transform.eulerAngles.z == 0)
            {
                nextAtkUpSprite = Time.time + atkSpriteCount;
                animator.SetFloat("attackUp", nextAtkUpSprite);
            }
            else if (Time.time > atkSpriteCount && atkPivot.transform.eulerAngles.z == 270)
            {
                nextAtkRightSprite = Time.time + atkSpriteCount;
                animator.SetFloat("attackRight", nextAtkRightSprite);
            }
            else if (Time.time > atkSpriteCount && atkPivot.transform.eulerAngles.z == 90)
            {
                nextAtkLeftSprite = Time.time + atkSpriteCount;
                animator.SetFloat("attackLeft", nextAtkLeftSprite);
            }
        }
    }

    void SpriteRend()
    {
        if(Time.time > backToDefault)
        {
            sprite.color = Color.white;
        }
    }

	IEnumerator Attack()
    {
        blockTrig.SetActive(false);
		atkPivot.SetActive(true);
        atkTrig.SetActive(true);
        animator.SetFloat("block", -1f);
        yield return new WaitForSeconds(atkDuration);
        atkTrig.SetActive(false);
        atkPivot.SetActive(false);
	}

    IEnumerator Dash()
    {
        if (!dashing)
        {
            dashing = true;
            speed = speed * 4;
            yield return new WaitForSeconds(0.2f);
            speed = speed / 4;
            dashing = false;
        }
    }

	public float getHealth(){
		return health;
	}

	public float getDamage(){
		return damage;
	}

    public void setThrowing(int i)
    {
        throwingInteger = i;
    }


    public void setKnockingBack()
    {
        if (!knockingBack)      { knockingBack = true; }
        else {
            throwingInteger = 0;
            knockingBack = false;
        }
    }

	public void setHealth(float damage){
        if (Time.time > backToDefault)
        {
            health -= damage;
            life.fillAmount -= 0.1f;
            sprite.color = Color.red;
            backToDefault = Time.time + damageSpriteTime;
        }
    }

}
