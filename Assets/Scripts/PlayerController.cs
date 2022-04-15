using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Summary: Control player actions - jumping and jump button, walking and exiting on game clear 

    private int jumpCount;
    private Rigidbody2D rigidbodyPlayer;
    private Animator animatorPlayer;
    private Image imgJumpButton;
    public float jumpPower;
    public GameManager GameManager;
    public GameObject objJumpButton;
    public Sprite spriteJumpButtonUp;
    public Sprite spriteJumpButtonDown;

    public void JumpButtonUp()
    {
        //Set jump button image to normal
        imgJumpButton.sprite = spriteJumpButtonUp;
    }

    private void JumpButtonDown()
    {
        //Set jump button image to pressed
        imgJumpButton.sprite = spriteJumpButtonDown;
    }

    public void Jump()
    {
        //Player jumps
        if (jumpCount < 2)
        {
            jumpCount += 1;
            animatorPlayer.SetBool("isJump", true);
            rigidbodyPlayer.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            GameManager.PlaySound("JUMP");
            JumpButtonDown();
        }
    }

    private void Walk()
    {
        //Player walks towards the signboard on Game clear
        Vector2 position = transform.position;
        position.x = position.x + 3.0f * Time.deltaTime;
        transform.position = position;
    }

    private void Awake()
    {
        jumpCount = 0;
        rigidbodyPlayer = gameObject.GetComponent<Rigidbody2D>();
        animatorPlayer = gameObject.GetComponent<Animator>();
        imgJumpButton = objJumpButton.GetComponent<Image>();
        
    }

    private void Update()
    {
        //This is for PC jump test
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        else if (Input.GetButtonUp("Jump"))
        {
            JumpButtonUp();
        }        
        //Inactivate jump button on game end
        if (GameManager.isGameEnd)
        {
            objJumpButton.SetActive(false);
        }
        //Player walk to the signboard on game end
        if (GameManager.isEndOfRoad)
        {
            Walk();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Stop jumping animation when player touch floor
        if (collision.gameObject.name == "Zone_floor")
        {
            jumpCount = 0;
            animatorPlayer.SetBool("isJump", false);
        }

        ///Exit game when player reach signboard
        if(collision.gameObject.name == "Signboard")
        {
            this.gameObject.SetActive(false);
            GameManager.ShowGameResult();
        }
    }
}
