using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Summary: Control player actions - jumping and jump button, joystick, walking and exiting on game clear 
    
    private int jumpCount;
    private int swimSpeed;
    private Vector3 initialPosition;
    private Rigidbody2D rigidbodyPlayer;
    private Animator animatorPlayer;
    private Image imgJumpButton;
    private SpriteRenderer playerSpriteRenderer;
    private JoyStickController JoyStickController;
    public bool isEnterSeaMode;
    public float jumpPower;
    public GameManager GameManager;
    public GameObject objCarMode;
    public GameObject objJoyStick;
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
            if (!GameManager.isCarMode)
            {
                animatorPlayer.SetBool("isJump", true);
            }
            else
            {
                animatorPlayer.SetBool("isCarJump", true);
            }

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
        swimSpeed = 8;
        isEnterSeaMode = false;
        initialPosition = transform.position;
        rigidbodyPlayer = gameObject.GetComponent<Rigidbody2D>();
        animatorPlayer = gameObject.GetComponent<Animator>();
        imgJumpButton = objJumpButton.GetComponent<Image>();
        playerSpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        JoyStickController = objJoyStick.transform.GetChild(0).GetComponent<JoyStickController>();
    }

    private void Start()
    {
        animatorPlayer.SetBool("isSwim", false);
        objJoyStick.SetActive(false);
        objCarMode.SetActive(false);
    }

    private void Update()
    {
        //This is for PC jump test
        if (Input.GetButtonDown("Jump"))
        {
            if (!GameManager.isSeaMode)
            {
                Jump();
            }

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

        //When Player enters the sea
        if (GameManager.isSeaMode && !isEnterSeaMode)
        {
            //Change Controller            
            objJumpButton.SetActive(false);
            objJoyStick.SetActive(true);

            //Control Physics
            transform.position = new Vector3(8f, -20f, 0f);
            rigidbodyPlayer.gravityScale = 0f;
            rigidbodyPlayer.velocity = new Vector3(1, 0, 0);

            //Change animation
            animatorPlayer.SetBool("isSwim", true);

            GameManager.isEnterSeaMode = true;
            isEnterSeaMode = true;
        }
        //Player enters car mode
        if (GameManager.isCarMode)
        {
            animatorPlayer.SetBool("isCar", true);
            objCarMode.SetActive(true);
        }
        //Player exits car mode
        if (GameManager.isExitCarMode)
        {
            objCarMode.SetActive(false);
            animatorPlayer.SetBool("isCar", false);
            GameManager.isExitCarMode = false;
        }
    }
    private void SeaModeMovement()
    {
        float x = JoyStickController.GetX();
        float y = JoyStickController.GetY();
        Vector3 swimDir = new Vector3(x, y, 0).normalized;
        transform.Translate(swimDir * swimSpeed * Time.deltaTime);
        
        //Face right side
        if(swimDir.x > 0)
        {
            playerSpriteRenderer.flipX = false;
        }
        //Face left
        else
        {
            playerSpriteRenderer.flipX = true;
        }
    }

    private void FixedUpdate()
    {
        //Player movement in sea mode
        if (GameManager.isSeaMode)
        {
            SeaModeMovement();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Stop jumping animation when player touch floor
        if (collision.gameObject.name == "Zone_floor")
        {
            jumpCount = 0;
            if (!GameManager.isCarMode)
            {
                animatorPlayer.SetBool("isJump", false);
            }
            else
            {
                animatorPlayer.SetBool("isCarJump", false);
            }

        }

        //Exit game when player reach signboard
        if(collision.gameObject.name == "Signboard")
        {
            this.gameObject.SetActive(false);
            GameManager.ShowGameResult();
        }
        
        //Return to city mode from sea mode if touch ladder
        if(collision.gameObject.name == "Ladder")
        {
            rigidbodyPlayer.velocity = Vector3.zero;            
            GameManager.isSeaMode = false;
            GameManager.isExitSeaMode = true;
            isEnterSeaMode = false;
            rigidbodyPlayer.gravityScale = 3f;
            transform.position = initialPosition;

            objJumpButton.SetActive(true);
            objJoyStick.SetActive(false);
            GameManager.objSea.SetActive(false);
            animatorPlayer.SetBool("isSwim", false);
            playerSpriteRenderer.flipX = false;
        }
    }
}
