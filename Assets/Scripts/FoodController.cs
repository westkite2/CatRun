using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    private SpriteRenderer SpriteRend;
    private Sprite[] SpriteList;
    private GameManager GameManager;
    private int lastSprite;
    private int spriteNum;

    void InitSprite(string Address)
    {
        SpriteList = Resources.LoadAll<Sprite>(Address);
        SpriteRend.sprite = SpriteList[0];
    }
    void Awake()
    {
        spriteNum = 0;
        SpriteRend = gameObject.GetComponent<SpriteRenderer>();
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //Set initial sprite
        switch (gameObject.name)
        {
            case "Food_egg":
                InitSprite("Sprites/food_egg");
                lastSprite = 2;
                break;
            case "Food_chicken":
                InitSprite("Sprites/food_chicken");
                lastSprite = 1;
                break;
        }
    }

    private void Update()
    {
        //Move towards the player
        transform.position = new Vector2(transform.position.x - 0.1f, transform.position.y);
    }

    private void OnMouseDown()
    {
        //Cook food
        if (spriteNum != lastSprite)
        {
            spriteNum += 1;
            SpriteRend.sprite = SpriteList[spriteNum];
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (spriteNum == lastSprite)
            {
                gameObject.SetActive(false);
                GameManager.score += 1;
            }
            else
            {
                gameObject.SetActive(false);
                GameManager.score -= 1;
            }

        }
        else if (collision.gameObject.name == "Zone_foodEnd")
        {
            gameObject.SetActive(false);
        }
    }

}
