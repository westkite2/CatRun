using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    private SpriteRenderer SpriteRend;
    private Sprite[] SpriteList;

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
    
    private void OnMouseDown()
    {
        if (spriteNum != lastSprite)
        {
            spriteNum += 1;
            SpriteRend.sprite = SpriteList[spriteNum];
        }
        else
        {
            Debug.Log("Already Last");
        }

        
    }

}
