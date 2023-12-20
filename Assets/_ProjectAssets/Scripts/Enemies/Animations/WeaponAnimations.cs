using System;using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimations
{
    protected AnimationController _daddy;
    
    private static readonly int IsShooting = Animator.StringToHash("isShooting");
    private static readonly int weaponType = Animator.StringToHash("weaponType");
    private static readonly int Reload = Animator.StringToHash("reload");
    private static readonly int Shoot1 = Animator.StringToHash("shoot");
    private static readonly int Property = Animator.StringToHash("reload 0");

    public WeaponAnimations(AnimationController controller)
    {
        _daddy = controller;
    }

    public void SetWeaponType(int type)
    {
        
        Debug.Log((Constants.EnemyType)type);
        //make this more modular
        if (type >= 2)
            type = 2;
        
        _daddy.animator.SetInteger(weaponType, type);
    }
    
    public void Shoot()
    {
        Debug.Log("shoot");
        _daddy.animator.SetTrigger(Shoot1);
    }

    public void StopShooting()
    {
        //_daddy.animator.SetBool(IsShooting, false);
    }

    public void StartReload()
    {
        _daddy.animator.SetTrigger(Property);
    }

    public void ReloadComplete()
    {
        _daddy.animator.SetBool(Reload, false);
    }
}
