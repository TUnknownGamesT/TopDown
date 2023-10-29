using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    
    private PlayerInput _playerMovement;
    private InputAction _moveAction;
    private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _playerMovement = new();
        _playerMovement.Enable();
        _moveAction = _playerMovement.Player.Movement;
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
