using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation : MonoBehaviour
{
    public float distanceOfPoint;
    
    private PlayerInput _playerMovement;
    private InputAction _mousePosition;
    
    Vector3 mousePosition = Vector3.zero;


    private void Awake()
    {
        _playerMovement = new();
        _playerMovement.Enable();
    }


    private void OnEnable()
    {
        _playerMovement.Player.MousePosition.performed += OnMousePosition;
    }
    
    private void OnDisable()
    {
        _playerMovement.Player.MousePosition.performed -= OnMousePosition;
    }

    private void OnMousePosition(InputAction.CallbackContext obj)
    {
        mousePosition = obj.ReadValue<Vector2>();
        mousePosition.z = distanceOfPoint;
        mousePosition =  Camera.main.ScreenToWorldPoint(mousePosition);
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayer();
    }

    private void RotatePlayer()
    {
        mousePosition.y = transform.position.y;
        transform.LookAt(mousePosition);
    }
}
