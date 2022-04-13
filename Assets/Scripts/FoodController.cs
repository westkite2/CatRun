using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    //Summary: Manage individual activated food - movement and player interactions

    private int totalSprite;
    private int spriteIndex;
    private Sprite[] spriteListFood;
    private SpriteRenderer spriteRendererFood;
    public GameObject objExplosion;
    private SpriteRenderer spriteRendererExplosion;
    private Animator animatorExplosion;
    public GameManager GameManager;

    private void InitSprite(string Address)
    {
        //Set food sprite to initial state
        spriteListFood = Resources.LoadAll<Sprite>(Address);
        spriteRendererFood.sprite = spriteListFood[0];
    }

    private void Awake()
    {
        spriteIndex = 0;
        objExplosion = gameObject.transform.GetChild(0).gameObject;
        spriteRendererFood = gameObject.GetComponent<SpriteRenderer>();
        spriteRendererExplosion = objExplosion.GetComponent<SpriteRenderer>();
        animatorExplosion = objExplosion.GetComponent<Animator>();
        
        //Set initial sprite
        switch (gameObject.name)
        {
            case "Food_egg(Clone)":
                InitSprite("Sprites/Foods/food_egg");
                totalSprite = 2;
                break;
            case "Food_chicken(Clone)":
                InitSprite("Sprites/Foods/food_chicken");
                totalSprite = 1;
                break;
        }
    }

    private void Update()
    {
        //Food move towards the player
        transform.position = new Vector2(transform.position.x - 4f * Time.deltaTime, transform.position.y);
    }

    private void InactivateFood()
    {
        //Reset food variables
        spriteIndex = 0;
        spriteRendererFood.sprite = spriteListFood[0];
        spriteRendererExplosion.sprite = null;
        animatorExplosion.ResetTrigger("Explode");
        gameObject.transform.position =
                new Vector2(Random.Range(14.0f, 25.0f), Random.Range(-2.3f, 2.6f));
        gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        //Cook food on click
        if (spriteIndex < totalSprite)
        {
            spriteIndex += 1;
            GameManager.PlaySound("COOK");
            spriteRendererFood.sprite = spriteListFood[spriteIndex];
            animatorExplosion.SetTrigger("Explode");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //When player eats(touches) the food, update score
        if (collision.gameObject.name == "Player")
        {
            //Increase score if food cook complete
            if (spriteIndex == totalSprite)
            {
                GameManager.currentScore += 0.1f;
                GameManager.PlaySound("EAT");
                InactivateFood();
            }
            //Decrease score if food cook incomplete
            else
            {
                GameManager.currentScore -= 0.1f;
                GameManager.PlaySound("DAMAGE");
                InactivateFood();
            }

        }
        //If food not eaten and reach end of screen, inactivate food
        else if (collision.gameObject.name == "Zone_foodEnd")
        {
            InactivateFood();
        }
    }

}