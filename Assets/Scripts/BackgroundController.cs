using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    //Summary: Move(scroll) background image to make walking illusion

    private int tmpIdx;
    private int backType;
    private float camWidth;
    private Vector2 curPos;
    private Vector2 nextPos;
    private Vector2 endSpritePos;
    private string objName;
    public int scrollCount;
    public int startIdx;
    public int endIdx;
    public float moveSpeed;
    public Transform[] Sprites;
    public GameManager GameManager;

    private void Start()
    {
        scrollCount = 1;
        camWidth = 2 * Camera.main.orthographicSize * Camera.main.aspect;
        objName = gameObject.name;

        switch (objName)
        {
            case "Back_building":
                backType = 1;
                moveSpeed = 1;
                break;
            case "Back_road":
                backType = 2;
                moveSpeed = 4;
                break;
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Debug.Log("scroll count: " + scrollCount);
        //Move Background
        if (!GameManager.isGameEnd)
        {
            if (!GameManager.isSeaMode)
            {
                SetSpeed();
                Move();
                Scroll();
            }
        }
    }

    private void SetSpeed()
    {
        //Speed up in car mode
        if (GameManager.isCarMode)
        {
            if (backType == 1)
            {
                moveSpeed = 8;
            }
            else
            {
                moveSpeed = 22;
            }
        }
        else
        {
            if (backType == 1)
            {
                moveSpeed = 3;
            }
            else
            {
                moveSpeed = 8;
            }
        }
    }
    private void Move()
    {
        //Background moves to the left
        curPos = transform.position;
        nextPos = Vector2.left * moveSpeed * Time.deltaTime;
        transform.position = curPos + nextPos;
    }

    private void Scroll()
    {
        if (Sprites[startIdx].position.x < camWidth * (-1))
        {
            //Reuse Sprite
            endSpritePos = Sprites[endIdx].localPosition;
            Sprites[startIdx].transform.localPosition = endSpritePos + Vector2.right * camWidth;
            scrollCount += 1;

            //Update Sprite Index            
            tmpIdx = endIdx;
            endIdx = startIdx;
            startIdx = startIdx + 1 > Sprites.Length - 1 ? 0 : startIdx + 1;
        }
    }
}