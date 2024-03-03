using System;
using MoreMountains.TopDownEngine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    
    private CharacterController _characterController;
    private FootStepsSound _footStepsSound;


    private void OnDisable()
    {
        // UserInputController._movementAction.started -= MakeFootStepsSound;
        // UserInputController._movementAction.canceled -= StopMakingFootStepsSound;
    }
    

    private void Awake()
    {
        _footStepsSound = GetComponent<FootStepsSound>();
        _characterController = GetComponent<CharacterController>();
    }
    
    private void Start()
    {
        // UserInputController._movementAction.started += MakeFootStepsSound;
        // UserInputController._movementAction.canceled += StopMakingFootStepsSound;
    }
    
    private void Update()
    {
        Move();
    }
    
    public void Die()
    {
        speed = 0;
    }
    
    private void Move()
    {
        Vector2 direction = UserInputController._movementAction.ReadValue<Vector2>();
        _characterController.Move(new Vector3(direction.x, 0, direction.y)*Time.deltaTime *speed);
        if (direction.magnitude != 0)
        {
            MakeFootStepsSound();
        }
        else
        {
            StopMakingFootStepsSound();
        }
    }
     
    private void MakeFootStepsSound()
    {
        _footStepsSound.StartWalking();
    }
    
    private void StopMakingFootStepsSound()
    {
        _footStepsSound.StopWalking();
    }
    
    
    
}
