using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    //Summary: Control camera movement

    private bool isEnterSeaMode;
    private float cameraMoveSpeed;
    private float camHeight;
    private float camWidth;
    private Vector2 mapSize;
    private Vector2 center;
    private Vector3 initialPosition;
    private PlayerController playerController;
    public GameManager GameManager;
    public GameObject objPlayer;

    private void Awake()
    {
        isEnterSeaMode = false;
        cameraMoveSpeed = 2f;
        mapSize = new Vector2(19, 10);
        center = new Vector2(8, -20);
        initialPosition = gameObject.transform.position;
    }

    private void Start()
    {
       playerController = objPlayer.GetComponent<PlayerController>();
       camHeight = Camera.main.orthographicSize;
       camWidth = camHeight * Screen.width / Screen.height;
    }

    private void Update()
    {
        //Initial position of the camera
        if (!GameManager.isSeaMode)
        {
            transform.position = initialPosition;
        }
    }

    private void CameraMovement()
    {
        //Limit camera area
        transform.position = Vector3.Lerp(transform.position,
                                          objPlayer.transform.position + initialPosition,
                                          Time.deltaTime * cameraMoveSpeed);
        float lx = mapSize.x - camWidth;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = mapSize.y - camHeight;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        //Move camera
        transform.position = new Vector3(clampX, clampY, -10f);
    }

    private void FixedUpdate()
    {
       if (GameManager.isSeaMode)
        {
            if (playerController.isEnterSeaMode)
            {
                //Move camera to the sea is enter sea mode
                if (!isEnterSeaMode)
                {
                    transform.position = objPlayer.transform.position + initialPosition;
                    isEnterSeaMode = true;
                }
                //Track player
                CameraMovement();
            }
        }
    }
}
