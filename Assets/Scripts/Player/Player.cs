using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player: MonoBehaviour, IEntity {

    private Rigidbody2D rb;
	private GameObject atkPivot;
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

    public Animator animator;

    // Use this for initialization
    void Start () {
		rb = this.gameObject.GetComponent<Rigidbody2D>();
		atkPivot = transform.GetChild(0).gameObject;
		atkPivot.SetActive (false);
        animator = GetComponent<Animator>();
    }

    void Update(){
		if (GameStory.reading) {
			rb.velocity = Vector2.zero;
			return;
		}
        Animation();
        Movement();
		Combat();
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
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (rb.velocity.x < -2.3 || rb.velocity.x > 0.35)
            {
                rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed * 10;
            }
        }
        else
        {
           if(rb.transform.position.x <= 6 && rb.transform.position.x >= 0.4 && rb.transform.position.y <= 4.6f && rb.transform.position.y >= -0.6f)
            {
                rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed;
                
            }
            else
            {
                rb.velocity = new Vector2(0,0);
                float positionX = Mathf.Clamp(rb.transform.position.x, 0.4f, 6f);
                float positionY = Mathf.Clamp(rb.transform.position.y, -0.6f, 4.6f);
                rb.transform.position = new Vector3(positionX, positionY, this.transform.position.z);
            }
        }
    }

	void Combat(){

		if(animator.GetCurrentAnimatorStateInfo(0).IsName("walk_right"))
        {
			atkPivot.transform.eulerAngles = new Vector3(0,0,270);
		}if(animator.GetCurrentAnimatorStateInfo(0).IsName("walk_left"))
        {
			atkPivot.transform.eulerAngles = new Vector3(0,0,90);
		}if(animator.GetCurrentAnimatorStateInfo(0).IsName("walk_upper"))
        {
			atkPivot.transform.eulerAngles = new Vector3(0,0,0);
		}if(animator.GetCurrentAnimatorStateInfo(0).IsName("walk_bottom"))
        {
			atkPivot.transform.eulerAngles = new Vector3(0,0,180);
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

	IEnumerator Attack(){
		atkPivot.SetActive(true);
		yield return new WaitForSeconds(atkDuration);
		atkPivot.SetActive(false);
	}

	public float getHealth(){
		return health;
	}

	public float getDamage(){
		return damage;
	}

	public void setHealth(float damage){
		health -= damage;
        life.fillAmount -= 0.1f;
	}

}
