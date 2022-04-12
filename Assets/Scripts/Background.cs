using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float speed;
    public int startIdx;
    public int endIdx;
    public Transform[] Sprites;

    private int tmpIdx;
    private float camWidth;
    private Vector2 curPos;
    private Vector2 nextPos;
    private Vector2 endSpritePos;
    private GameManager GameManager;

    private void Awake()
    {
        camWidth = 2 * Camera.main.orthographicSize * Camera.main.aspect;
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!GameManager.flag_backStop)
        {
            //Background movement
            Move();
            Scroll();
        }
    }

    private void Move()
    {
        //Background moves to the left
        curPos = transform.position;
        nextPos = Vector2.left * speed * Time.deltaTime;
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
