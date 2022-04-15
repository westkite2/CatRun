using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 initialPosition;
    public GameManager GameManager;
    public GameObject objPlayer;

    void Awake()
    {
        initialPosition = gameObject.transform.position;
    }

    private void Update()
    {
        if (!GameManager.isSeaMode)
        {
            transform.position = initialPosition;
        }
    }

    void FixedUpdate()
    {
       if (GameManager.isSeaMode)
        {
            transform.position = objPlayer.transform.position + initialPosition;
        }
    }
}
