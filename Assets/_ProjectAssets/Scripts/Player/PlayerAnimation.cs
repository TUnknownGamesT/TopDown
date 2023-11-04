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

    private Vector3 _oldPosition;
    
    private static readonly int IsShooting = Animator.StringToHash("isShooting");
    private static readonly int LegsMovement = Animator.StringToHash("legsMovement");

    private void Start()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();
        _playerInput.Player.Shooting.started += OnShoot;
        _playerInput.Player.Shooting.canceled += StopShooting;
    }
    
    void Update() {
        Vector3 direction = transform.position - _oldPosition;
        float forwardTest = Vector3.Dot(-direction.normalized, transform.position.normalized);

        if(forwardTest  > 0) {
            playerAnimator.SetFloat(LegsMovement, 1);
        } else if (forwardTest < 0) {
            playerAnimator.SetFloat(LegsMovement, 0);
        } else {
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
