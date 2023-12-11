using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerAnimation : AnimationController
{
    private PlayerInput _playerInput;
    private InputAction _inputAction;
    private WeaponAnimations _myWeapon;
    
    protected override void Start()
    {
        base.Start();
        _myWeapon = new WeaponAnimations(this);
        _myWeapon.SetWeaponType(1);
        UserInputController._leftClick.performed  += OnShoot;
        UserInputController._leftClick.canceled += StopShooting;
    }

    void Update()
    {
        SetWalkingAnimation();
    }
    
    
    private void OnShoot(InputAction.CallbackContext obj)
    {
        _myWeapon.Shoot();
    }
    private void StopShooting(InputAction.CallbackContext obj)
    {
        _myWeapon.StopShooting();
    }
}
