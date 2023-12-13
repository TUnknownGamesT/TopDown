using System;using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimations
{
    protected AnimationController _daddy;
    
    private static readonly int IsShooting = Animator.StringToHash("isShooting");
    private static readonly int weaponType = Animator.StringToHash("weaponType");

    public WeaponAnimations(AnimationController controller)
    {
        _daddy = controller;
    }

    public void SetWeaponType(int type)
    {
        _daddy.animator.SetInteger(weaponType, type);
    }
    
    public void Shoot()
    {
        _daddy.animator.SetBool(IsShooting, true);
    }

    public void StopShooting()
    {
        _daddy.animator.SetBool(IsShooting, false);
    }
}
