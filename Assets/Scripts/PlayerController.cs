using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float jumpPower;
    public GameObject JumpButton;
    public Sprite Sprite_JumpButtonUp;
    public Sprite Sprite_JumpButtonDown;

    private int jumpCnt;
    private Rigidbody2D Rigid;
    private Animator Anim;
    private GameManager GameManager;
    private Image JumpImage;
    
    public void JumpButtonUp()
    {
        JumpImage.sprite = Sprite_JumpButtonUp;
    }

    private void JumpButtonDown()
    {
        JumpImage.sprite = Sprite_JumpButtonDown;
    }

    public void Jump()
    {
        //Player jumps
        if (jumpCnt < 2)
        {
            GameManager.PlaySound("JUMP");
            jumpCnt += 1; 
            Anim.SetBool("isJump", true);
            Rigid.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            JumpButtonDown();
        }
    }

    private void Walk()
    {
        //Walk towards the signboard on Game clear
        Vector2 position = transform.position;
        position.x = position.x + 3.0f * Time.deltaTime;
        transform.position = position;
    }

    private void Awake()
    {
        jumpCnt = 0;

        Rigid = gameObject.GetComponent<Rigidbody2D>();
        Anim = gameObject.GetComponent<Animator>();
        JumpImage = JumpButton.GetComponent<Image>();
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            //This is for PC jump test
            Jump();
        }
        if (Input.GetButtonUp("Jump"))
        {
            //This is for PC jump test
            JumpButtonUp();
        }

        if (GameManager.flag_gameClear)
        {
            JumpButton.SetActive(false);
        }
        if (GameManager.flag_backStop)
        {
            Walk();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Zone_floor")
        {
            //Stop jumping animation when touch floor
            jumpCnt = 0;
            Anim.SetBool("isJump", false);
        }
        if(collision.gameObject.name == "Signboard")
        {
            //Player exits the scene
            this.gameObject.SetActive(false);
            //Exit Game
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainScene");
        }
    }
}
