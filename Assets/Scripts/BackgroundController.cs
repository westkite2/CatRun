using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    //Summary: Move(scroll) background image to make walking illusion
    private int tmpIdx;
    private float camWidth;
    private Vector2 curPos;
    private Vector2 nextPos;
    private Vector2 endSpritePos;

    public int startIdx;
    public int endIdx;
    public float moveSpeed;
    public Transform[] Sprites;
    public GameManager GameManager;

    private void Start()
    {
        camWidth = 2 * Camera.main.orthographicSize * Camera.main.aspect;
    }

    // Update is called once per frame
    private void Update()
    {
        //Move Background
        if (!GameManager.isBackgroundStop)
        {
            Move();
            Scroll();
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

            //Update Sprite Index            
            tmpIdx = endIdx;
            endIdx = startIdx;
            startIdx = startIdx + 1 > Sprites.Length - 1 ? 0 : startIdx + 1;
        }
    }
}