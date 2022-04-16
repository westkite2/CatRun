using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkController : MonoBehaviour
{
    //Summary: Control shark movement
    private bool isFlip;
    private int direction;
    private SpriteRenderer spriteRenderer;
    public float speed=1;
    public GameManager GameManager;

    void Start()
    {
        isFlip = false;
        direction = 1;
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        //Shark swims toward the wall
        transform.position = new Vector2(transform.position.x + speed * direction * Time.deltaTime, transform.position.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Wall")
        {
            ChangeDirection();
        }
        if (collision.gameObject.name == "Player")
        {
            GameManager.currentHp -= 30;
            ChangeDirection();
        }
    }
    private void ChangeDirection()
    {
        if (isFlip)
        {
            spriteRenderer.flipX = false;
            isFlip = false;
            direction = 1;
        }
        else
        {
            spriteRenderer.flipX = true;
            isFlip = true;
            direction = -1;
        }

    }
}
