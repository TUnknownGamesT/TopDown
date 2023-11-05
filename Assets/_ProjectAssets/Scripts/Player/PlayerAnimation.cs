using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator playerAnimator;
    
    private PlayerInput _playerInput;
    private InputAction _inputAction;

    public Gunn gun;
    private Vector3 _oldPosition;
    
    private static readonly int IsShooting = Animator.StringToHash("isShooting");
    private static readonly int LegsMovement = Animator.StringToHash("legsMovement");

    private void Start()
    {
        UserInputController._leftClick.performed  += OnShoot;
        UserInputController._leftClick.canceled += StopShooting;
    }
    
    void Update() {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 difference = _oldPosition - transform.position;
        float directionValue = Vector3.Dot(forward, difference);
        if (directionValue < -0.001f)
        {
            playerAnimator.SetFloat(LegsMovement, 1);
        }
        else if (directionValue>0.001f)
        {
            playerAnimator.SetFloat(LegsMovement, 0);
        }
        else
        {
            playerAnimator.SetFloat(LegsMovement, 0.5f);
        }
        _oldPosition = transform.position;
    }
    
    
    private void OnShoot(InputAction.CallbackContext obj)
    {
        playerAnimator.SetBool(IsShooting, true);
    }
    private void StopShooting(InputAction.CallbackContext obj)
    {
        playerAnimator.SetBool(IsShooting, false);
    }

}
