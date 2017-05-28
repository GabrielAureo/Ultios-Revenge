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
    public Image life;

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

    // Use this for initialization
    void Start () {
		rb = this.gameObject.GetComponent<Rigidbody2D>();
        sprite = this.gameObject.GetComponent<SpriteRenderer>();
		atkPivot = transform.GetChild(0).gameObject;
        atkTrig = transform.GetChild(0).GetChild(0).gameObject;
        blockTrig = transform.GetChild(0).GetChild(1).gameObject;
        atkPivot.SetActive(false);
        animator = GetComponent<Animator>();
        animator.SetFloat("blockway", 5f);
    }

    void Update(){
		if (GameStory.reading || GameOver.gameOver) {
			rb.velocity = Vector2.zero;
			return;
		}

        SpriteRend();
        Animation();
        Movement();
		Combat();
        if (atkTrig.activeSelf == false)
        {
            Blocked();
        }
        speedDecressing();
        //Debug.Log(rb.transform.position);
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

    void Animation()
    {
     
        animator.SetFloat("attackBot", (nextAtkBotSprite - Time.time));
        animator.SetFloat("attackUp", (nextAtkUpSprite - Time.time));
        animator.SetFloat("attackRight", (nextAtkRightSprite - Time.time));
        animator.SetFloat("attackLeft", (nextAtkLeftSprite - Time.time));
        animator.SetBool("downMovement", false);

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            animator.SetFloat("stay", 0f);
            animator.SetFloat("walk", 1f);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            animator.SetFloat("stay", 0f);
            animator.SetFloat("walk", 7f);
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            animator.SetFloat("stay", 0f);
            animator.SetFloat("walk", 5f);
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            animator.SetFloat("stay", 0f);
            animator.SetFloat("walk", 3f);
        }
        else
        {
            animator.SetFloat("stay", 2f);
            animator.SetFloat("walk", 0f);
        }
        if (Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxisRaw("Vertical") < 0)
        {
            animator.SetBool("downMovement", true);
        }
        else if(Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") < 0)
        {
            animator.SetBool("downMovement", true);
        }
        else if (Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") > 0)
        {
            animator.SetBool("downMovement", true);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxisRaw("Vertical") > 0)
        {
            animator.SetBool("downMovement", true);
        }
    }

	void Movement(){
        if (!blocking)
        {
            if (Input.GetKeyDown(KeyCode.Mouse2))
            {
                //if (rb.transform.position.x <= 6 && rb.transform.position.x >= 0.4 && rb.transform.position.y <= 4.6f && rb.transform.position.y >= -0.6f)
                // {
                StartCoroutine(Dash());
                //}
            }
            else
            {

                // if(rb.transform.position.x <= 6 && rb.transform.position.x >= 0.4 && rb.transform.position.y <= 4.6f && rb.transform.position.y >= -0.6f && !dashing)
                //{
                rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed;
                //}
                // else
                //{
                //    rb.velocity = new Vector2(0,0);
                //     float positionX = Mathf.Clamp(rb.transform.position.x, 0.4f, 6f);
                //     float positionY = Mathf.Clamp(rb.transform.position.y, -0.6f, 4.6f);
                //     rb.transform.position = new Vector3(positionX, positionY, this.transform.position.z);
                // }


            }
        }else
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
            speedFixed = speed;
            yield return new WaitForSeconds(0.2f);
            speed = speed / 4;
            dashing = false;
        }
    }

    public void speedDecressing()
    {
        if (dashing)
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speedFixed;
            speedFixed = speed - (speedFixed / 2);
        }
    }

	public float getHealth(){
		return health;
	}

	public float getDamage(){
		return damage;
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
