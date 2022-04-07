using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpPower;
    private int jumpCnt;
    private Rigidbody2D Rigid;
    

    // Start is called before the first frame update
    void Start()
    {
        jumpCnt = 0;
        Rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && jumpCnt < 2)
        {
            jumpCnt += 1;
            Rigid.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Zone_floor")
        {
            jumpCnt = 0;
        }
    }
}
