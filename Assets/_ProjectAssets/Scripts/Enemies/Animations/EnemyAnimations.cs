using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAnimations : AnimationController
{
    public WeaponAnimations myWeapon;
    
    
    private int WeaponType;


    protected override void Start()
    {
        base.Start();
        animator = transform.GetChild(0).GetComponent<Animator>();
        
    }

    private void Update()
    {
        SetWalkingAnimation();
    }

    public void SetWeapon(Constants.EnemyType enemyType)
    {
        myWeapon = new WeaponAnimations(this);
        if (gameObject.GetComponent<EnemyMele>()!=null)
        {
            myWeapon.SetWeaponType(0);
            WeaponType = 0;
        }
        else if (gameObject.GetComponent<EnemyRange>()!=null)
        {
            myWeapon.SetWeaponType((int)enemyType);
            WeaponType = (int)enemyType;
        }
    }
}
