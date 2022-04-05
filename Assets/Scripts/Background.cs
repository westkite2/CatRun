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
    private Vector3 curPos;
    private Vector3 nextPos;
    private Vector3 startSpritePos;
    private Vector3 endSpritePos;

    private void Awake()
    {
        camWidth = 2 * Camera.main.orthographicSize * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        //move
        curPos = transform.position;
        nextPos = Vector3.left * speed * Time.deltaTime;
        transform.position = curPos + nextPos;

        if(Sprites[startIdx].position.x < camWidth * (-1))
        {
            //startSpritePos = Sprites[startIdx].localPosition;
            endSpritePos = Sprites[endIdx].localPosition;
            Sprites[startIdx].transform.localPosition = endSpritePos + Vector3.right * camWidth;

            //Update Sprite Index            
            tmpIdx = endIdx;
            endIdx = startIdx;
            startIdx = startIdx + 1 > Sprites.Length - 1 ? 0 : startIdx + 1;
        }
    }
}
