using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation : MonoBehaviour
{
    public float distanceOfPoint;
    public GameObject cube;

    public Vector3 mousePosition = Vector3.zero;

    
    void Update()
    {
        RotatePlayer();
    }

    private void RotatePlayer()
    {
        mousePosition = Input.mousePosition;
        mousePosition.z = distanceOfPoint;
        mousePosition =  Camera.main.ScreenToWorldPoint(mousePosition);
        
        mousePosition.y = transform.position.y;
        cube.transform.position = mousePosition;
        transform.LookAt(mousePosition);
    }
}
