using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    
    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _playerInput = new();
        _playerInput.Enable();
        _moveAction = _playerInput.Player.Movement;
    }

    private void Update()
    {
        Move();
    }


    private void Move()
    {
        Vector2 direction = _moveAction.ReadValue<Vector2>();
        _characterController.Move(new Vector3(direction.x, 0, direction.y)*Time.deltaTime *speed);
    }

   
}
