using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float cameraMoveSpeed;
    private float camHeight;
    private float camWidth;
    private Vector2 mapSize;
    private Vector2 center;
    private Vector3 initialPosition;
    public GameManager GameManager;
    public GameObject objPlayer;

    private void Awake()
    {
        cameraMoveSpeed = 1f;
        mapSize = new Vector2(38, 22);
        center = new Vector2(8, -20);
        initialPosition = gameObject.transform.position;
    }
    private void Start()
    {
       camHeight = Camera.main.orthographicSize;
       camWidth = camHeight * Screen.width / Screen.height;
    }
    private void Update()
    {
        if (!GameManager.isSeaMode)
        {
            transform.position = initialPosition;
        }
    }

    private void LimitCameraArea()
    {
        transform.position = Vector3.Lerp(transform.position,
                                          objPlayer.transform.position + initialPosition,
                                          Time.deltaTime * cameraMoveSpeed);
        float lx = mapSize.x - camWidth;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = mapSize.y - camHeight;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }

    private void FixedUpdate()
    {
       if (GameManager.isSeaMode)
        {
            //LimitCameraArea();
            transform.position = objPlayer.transform.position + initialPosition;
        }
    }
}
