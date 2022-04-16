using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoyStickController : MonoBehaviour
{
    private float distance;
    private float radius;
    private Vector3 stickPos;
    private Vector3 mousePos;
    private Vector3 mouseDir;
    
    private void Start()
    {
        radius = 25;
        stickPos = transform.position;
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            mousePos = Input.mousePosition;            
            mouseDir = (mousePos - stickPos).normalized;
            distance = Vector3.Distance(mousePos, stickPos);
            if(distance < radius)
            {
                transform.position = stickPos + mouseDir * distance;
            }
            else
            {
                transform.position = stickPos + mouseDir * radius;
            }
            
        }
        else
        {
            transform.position = stickPos;
        }

    }
    public float GetX()
    {
        return mouseDir.x;
    }
    public float GetY()
    {
        return mouseDir.y;
    }
}
