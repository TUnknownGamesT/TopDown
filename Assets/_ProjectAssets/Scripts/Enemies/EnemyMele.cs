using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class EnemyMele : EnemyArms
{

    protected override bool CanShoot()
    {
        try
        {
            if(Vector3.Distance(transform.position,GameManager.playerRef.position) < GetComponent<EnemyMovement>().stoppingDistance )
            {
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

    protected override void InitStats(EnemyType enemyType)
    {
        damage = enemyType.damage;
        timeBetweenShoots = enemyType.timeBetweenShoots;
        Instantiate(enemyType.armPrefab, armSpawnPoint.position, armSpawnPoint.rotation,transform);
    }
}
