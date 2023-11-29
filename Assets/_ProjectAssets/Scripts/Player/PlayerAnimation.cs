using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator playerAnimator;

    private PlayerInput _playerInput;
    private InputAction _inputAction;

    public Gunn gun;
    private Vector3 _oldPosition;

    private float _animationMultiplier=30;
    
    private static readonly int IsShooting = Animator.StringToHash("isShooting");
    private static readonly int frontWalking = Animator.StringToHash("frontWalking");
    private static readonly int sideWalking = Animator.StringToHash("sideWalking");

    private Rigidbody[] rigidbodies;
    
    private void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        SetRagdollEnabled(false);
        
        UserInputController._leftClick.performed  += OnShoot;
        UserInputController._leftClick.canceled += StopShooting;
    }
    
    private void SetRagdollEnabled(bool isEnabled)
    {
        
        foreach (var rb in rigidbodies)
        {
            rb.isKinematic = !isEnabled;
            rb.detectCollisions = isEnabled;
        }
        if (isEnabled)
        {
            playerAnimator.enabled = false;
        }
    }
    
    void Update()
    {
        
        Vector3 newToOld = _oldPosition - transform.position;

        float directionValue = Vector3.Dot(transform.forward, newToOld);
        playerAnimator.SetFloat(frontWalking, directionValue*_animationMultiplier);
        
        directionValue = Vector3.Dot(transform.right, newToOld);
        playerAnimator.SetFloat(sideWalking, directionValue*_animationMultiplier);
        
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

    [ContextMenu("die")]
    public void Die()
    {
        SetRagdollEnabled(true);
    }

}
