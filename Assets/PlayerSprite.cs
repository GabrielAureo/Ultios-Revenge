using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{

    public Sprite firstSprite;
    public Sprite secondSprite;
    public Sprite thirdSprite;
    public Sprite fourthSprite;

    private static int count = 0;

    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer.sprite == null)
            spriteRenderer.sprite = firstSprite;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W)) // If the space bar is pushed down
        {
            ChangeTheDamnSprite(); // call method to change sprite
        }
    }

    void ChangeTheDamnSprite()
    {
        if (count < 5) // if the spriteRenderer sprite = sprite1 then change to sprite2
        {
            spriteRenderer.sprite = secondSprite;
            count++;
        }
        else if (count < 10) // if the spriteRenderer sprite = sprite1 then change to sprite2
        {
            spriteRenderer.sprite = thirdSprite;
            count++;
        }
        else if (count < 15) // if the spriteRenderer sprite = sprite1 then change to sprite2
        {
            spriteRenderer.sprite = fourthSprite;
            count++;
        }
        else if (count < 20) // if the spriteRenderer sprite = sprite1 then change to sprite2
        {
            spriteRenderer.sprite = firstSprite;
            count++;
        }
        else if (count == 20)
        {
            count = 0;
        }
    }
}
