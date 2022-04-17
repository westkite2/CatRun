using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    //Summary: Control individual activated food - movement and player interactions

    private int totalSprite;
    private int spriteIndex;
    private int moveSpeed;
    private int foodType;
    private Sprite[] spriteListFood;
    private SpriteRenderer spriteRendererFood;
    private SpriteRenderer spriteRendererExplosion;
    private SpriteRenderer spriteRendererComplete;
    private Animator animatorExplosion;
    private Animator animatorComplete;
    public GameObject objExplosion;
    public GameObject objComplete;
    public GameManager GameManager;

    private void InitSprite(string Address)
    {
        //Set food sprite to initial state
        spriteListFood = Resources.LoadAll<Sprite>(Address);
        spriteRendererFood.sprite = spriteListFood[0];
    }

    private void SetSpeed()
    {
        if (!GameManager.isCarMode)
        {
            moveSpeed = 7;
        }
        else
        {
            moveSpeed = 18;
        }
    }

    private void CookForCarMode()
    {
        spriteIndex = totalSprite;
        spriteRendererFood.sprite = spriteListFood[spriteIndex];
    }

    private void CollectFood()
    {
        switch(foodType){
            case 0:
                GameManager.countFood[0] += 1;
                break;
            case 1:
                GameManager.countFood[1] += 1;
                break;
            case 2:
                GameManager.countFood[2] += 1;
                break;
            case 3:
                GameManager.countFood[3] += 1;
                break;
            case 4:
                GameManager.countFood[4] += 1;
                break;
        }
    }
    private void Awake()
    {
        spriteIndex = 0;
        objExplosion = gameObject.transform.GetChild(0).gameObject;
        objComplete = gameObject.transform.GetChild(1).gameObject;
        spriteRendererExplosion = objExplosion.GetComponent<SpriteRenderer>();
        spriteRendererComplete = objComplete.GetComponent<SpriteRenderer>();
        animatorExplosion = objExplosion.GetComponent<Animator>();
        animatorComplete = objComplete.GetComponent<Animator>();
        spriteRendererFood = gameObject.GetComponent<SpriteRenderer>();

        //Set initial sprite
        switch (gameObject.name)
        {
            case "Salmon(Clone)":
                foodType = 0;
                InitSprite("Sprites/Foods/salmon");
                totalSprite = 2;
                break;
            case "Shrimp(Clone)":
                foodType = 1;
                InitSprite("Sprites/Foods/shrimp");
                totalSprite = 2;
                break;
            case "Tuna(Clone)":
                foodType = 2;
                InitSprite("Sprites/Foods/tuna");
                totalSprite = 2;
                break;
            case "Egg(Clone)":
                foodType = 3;
                InitSprite("Sprites/Foods/egg");
                totalSprite = 2;
                break;
            case "FishEgg(Clone)":
                foodType = 4;
                InitSprite("Sprites/Foods/fishegg");
                totalSprite = 1;
                break;
        }
    }
    
    private void Update()
    {
        //Food move towards the player
        if (!GameManager.isSeaMode)
        {
            SetSpeed();
            transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);

            if (GameManager.isCarMode)
            {
                CookForCarMode();
            }
        }
    }

    private void InactivateFood()
    {
        //Reset food variables
        spriteIndex = 0;
        spriteRendererFood.sprite = spriteListFood[0];
        spriteRendererExplosion.sprite = null;
        spriteRendererComplete.sprite = null;
        animatorExplosion.ResetTrigger("Explode");
        animatorComplete.ResetTrigger("Complete");
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

            //cook animation
            animatorExplosion.SetTrigger("Explode");
            if(spriteIndex == totalSprite)
            {
                animatorComplete.SetTrigger("Complete");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //When player eats(touches) the food, update hp
        if (collision.gameObject.name == "Player")
        {
            //Collect food if food cook complete
            if (spriteIndex == totalSprite)
            {
                CollectFood();
                GameManager.PlaySound("EAT");
                InactivateFood();
            }
            //Decrease hp if food cook incomplete
            else
            {
                GameManager.currentHp -= 2;
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