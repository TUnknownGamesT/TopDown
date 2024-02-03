using System;using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimations
{
    protected AnimationController _daddy;
    protected int handsOnWeapon;
    
    private static readonly int weaponType = Animator.StringToHash("weaponType");
    private static readonly int Reload = Animator.StringToHash("reload");
    private static readonly int Property = Animator.StringToHash("reload 0");

    public WeaponAnimations(AnimationController controller)
    {
        _daddy = controller;
    }

    public void SetWeaponType(int type)
    {
        handsOnWeapon = type;
        _daddy.animator.SetInteger(weaponType, type);
    }
    
    public void Shoot()
    {
        if (handsOnWeapon==1)
        {
            _daddy.animator.Play("1handShoot");
        }
        else
        {
            _daddy.animator.Play("2handShoot",0,0);
        }
        
        //_daddy.animator.SetTrigger(Shoot1);
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
