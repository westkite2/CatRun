using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    //Summary: Manage items - hpIncrease, hpDecrease

    private string itemName;
    private int itemType; //1: hp increase, 2: hp decrease
    public GameManager GameManager;

    private void Awake()
    {
        itemName = gameObject.name;
        itemType = 0;
    }
    private void Start()
    {
        switch (itemName)
        {
            case "HpItem":
                itemType = 1;
                break;
            case "BadFood":
                itemType = 2;
                break;
        }
    }

    void Update()
    {
        //Item moves toward the player
        transform.position = new Vector2(transform.position.x - 4f * Time.deltaTime, transform.position.y);
    }
    private void InactivateItem()
    {
        //Reset food variables
        gameObject.transform.position =
                new Vector2(Random.Range(14.0f, 25.0f), Random.Range(-2.3f, 2.6f));
        gameObject.SetActive(false);
    }
    private void OnMouseDown()
    {
        //Increase hp if the case
        if (itemType == 1)
        {
            if (GameManager.currentHp <= 80f)
            {
                GameManager.currentHp += 20f;
            }
            else
            {
                GameManager.currentHp = 100f;
            }
            GameManager.PlaySound("EAT");
            InactivateItem();
        }

        //Decrease hp if the case
        if (itemType == 2)
        {
            GameManager.currentHp -= 10f;
            GameManager.PlaySound("DAMAGE");
            InactivateItem();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //When player eats(touches) the item, update hp
        if (collision.gameObject.name == "Player")
        {
            //Increase hp if the case
            if(itemType == 1)
            {
                if(GameManager.currentHp <= 80f)
                {
                    GameManager.currentHp += 20f;
                }
                else
                {
                    GameManager.currentHp = 100f;
                }
                GameManager.PlaySound("EAT");
                InactivateItem();
            }

            //Decrease hp if the case
            if(itemType == 2)
            {
                GameManager.currentHp -= 10f;
                GameManager.PlaySound("DAMAGE");
                InactivateItem();
            }

        }
        //If food not eaten and reach end of screen, inactivate food
        else if (collision.gameObject.name == "Zone_foodEnd")
        {
            InactivateItem();
        }
    }
}
