using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyMele : EnemyArms
{

    private float stoppingDistacne;
    public GameObject knife;
    
    protected override bool CanShoot()
    {
        try
        {
            if(Vector3.Distance(transform.position,GameManager.playerRef.position) < GetComponent<EnemyMovement>().stoppingDistance )
            {
                Debug.Log("Can shoot");
                return true;
            }
        }
        catch (Exception e)
        {
            Debug.Log("Alta carpeala cuaie");
        }
        return false;
    }

    public override void Shoot()
    {
        UniTask.Void(async () =>
        {
            if( CanShoot())
            {
                GameManager.playerRef.GetComponent<PlayerHealth>().TakeDamage(damage);
                await UniTask.Delay(TimeSpan.FromSeconds(timeBetweenShoots));
            }

            if (_fieldOfView.canSeePlayer)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
                Shoot();
            }
        });
    }

    public override void InitStats(EnemyType enemyType)
    {
        damage = enemyType.damage;
        timeBetweenShoots = enemyType.timeBetweenShoots;
        Instantiate(enemyType.armPrefab, armSpawnPoint.position, Quaternion.identity,armSpawnPoint.transform);
    }

    public override void DropArm()
    {
        GameObject arm = EnemyInitiator.instance.GetKnife();
        Instantiate(arm,transform.position,Quaternion.identity); 
    }
}
