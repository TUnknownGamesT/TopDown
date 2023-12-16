using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAnimations : AnimationController
{
    public int WeaponType;
    private WeaponAnimations myWeapon;
    protected override void Start()
    {
        base.Start();
        SetWeapon();
    }

    private void Update()
    {
        SetWalkingAnimation();
    }

    void SetWeapon()
    {
        myWeapon = new WeaponAnimations(this);
        if (gameObject.GetComponent<EnemyMele>()!=null)
        {
            myWeapon.SetWeaponType(0);
            WeaponType = 0;
        }
        else if (gameObject.GetComponent<EnemyRange>()!=null)
        {
            myWeapon.SetWeaponType((int)GetComponent<EnemyRange>().enemyType);
            WeaponType = (int)GetComponent<EnemyRange>().enemyType;
        }
    }
}
