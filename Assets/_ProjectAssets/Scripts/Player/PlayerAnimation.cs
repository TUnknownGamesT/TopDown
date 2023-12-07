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
    
    [SerializeField]
    private Transform _playerTransform;

    private PlayerInput _playerInput;
    private InputAction _inputAction;

    public Gunn gun;
    private Vector3 _oldPosition;

    private float _animationMultiplier=30;
    
    private static readonly int IsShooting = Animator.StringToHash("isShooting");
    private static readonly int frontWalking = Animator.StringToHash("frontWalking");
    private static readonly int sideWalking = Animator.StringToHash("sideWalking");

    public Rigidbody[] rigidbodies;
    
    private void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        SetRagdollEnabled(false);
        
        UserInputController._leftClick.performed  += OnShoot;
        UserInputController._leftClick.canceled += StopShooting;
    }
    
    public void SetRagdollEnabled(bool isEnabled)
    {
        if (isEnabled)
        {
            playerAnimator.enabled = false;
        }
        
        foreach (var rb in rigidbodies)
        {
            rb.isKinematic = !isEnabled;
            rb.detectCollisions = isEnabled;
        }
        
    }
    
    void Update()
    {
        
        Vector3 newToOld = _oldPosition - _playerTransform.position;

        float directionValue = Vector3.Dot(_playerTransform.forward, newToOld);
        playerAnimator.SetFloat(frontWalking, directionValue*_animationMultiplier);
        
        directionValue = Vector3.Dot(_playerTransform.right, newToOld);
        playerAnimator.SetFloat(sideWalking, directionValue*_animationMultiplier);
        
        _oldPosition = _playerTransform.position;
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
    
    public void Explode()
    {
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        playerAnimator.enabled = false;
        
        
        foreach (var rb in rigidbodies)
        {
            rb.isKinematic = false;
            rb.detectCollisions = true;
        }
        
    }

}
