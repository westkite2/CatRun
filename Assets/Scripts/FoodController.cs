using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    private SpriteRenderer SpriteRend;
    private Sprite[] SpriteList;
    private GameManager GameManager;
    private GameObject Explosion;
    private SpriteRenderer SpriteRend_Explosion;
    private Animator Anim_Explosion;
    private int lastSprite;
    private int spriteNum;

    private void InitSprite(string Address)
    {
        SpriteList = Resources.LoadAll<Sprite>(Address);
        SpriteRend.sprite = SpriteList[0];
    }
    private void Awake()
    {
        spriteNum = 0;
        SpriteRend = gameObject.GetComponent<SpriteRenderer>();
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Explosion = gameObject.transform.GetChild(0).gameObject;
        SpriteRend_Explosion = Explosion.GetComponent<SpriteRenderer>();
        Anim_Explosion = Explosion.GetComponent<Animator>();

        //Set initial sprite
        switch (gameObject.name)
        {
            case "Food_egg(Clone)":
                InitSprite("Sprites/Foods/food_egg");
                lastSprite = 2;
                break;
            case "Food_chicken(Clone)":
                InitSprite("Sprites/Foods/food_chicken");
                lastSprite = 1;
                break;
        }
    }

    private void Update()
    {
        //Move towards the player
        transform.position = new Vector2(transform.position.x - 4f * Time.deltaTime, transform.position.y);
    }

    private void OnMouseDown()
    {
        //Cook food
        if (spriteNum < lastSprite)
        {
            spriteNum += 1;
            GameManager.PlaySound("COOK");
            SpriteRend.sprite = SpriteList[spriteNum];
            Anim_Explosion.SetTrigger("Explode");
        }

    }
    private void InactivateFood()
    {
        gameObject.SetActive(false);
        Anim_Explosion.ResetTrigger("Explode");
        spriteNum = 0;
        SpriteRend.sprite = SpriteList[0];
        SpriteRend_Explosion.sprite = null;
        gameObject.transform.position =
                new Vector2(Random.Range(14.0f, 25.0f), Random.Range(-2.3f, 2.6f));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (spriteNum == lastSprite) //Cook success
            {
                GameManager.score += 0.1f;
                GameManager.PlaySound("EAT");
                InactivateFood();
            }
            else //Cook failure
            {
                GameManager.score -= 0.1f;
                GameManager.PlaySound("DAMAGE");
                InactivateFood();
            }

        }
        else if (collision.gameObject.name == "Zone_foodEnd")
        {
            InactivateFood();
        }
    }

}
