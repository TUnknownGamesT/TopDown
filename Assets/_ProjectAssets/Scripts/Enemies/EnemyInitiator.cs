using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemyInitiator : MonoBehaviour
{

    #region Singleton

    public static EnemyInitiator instance;

    private void Awake()
    {
        instance = FindObjectOfType<EnemyInitiator>();
        if (instance == null)
        {
            instance = this;
        }
    }

    #endregion

    public List<EnemyType> enemyTypes;
    public List<GameObject> enemyLootArms;

    public EnemyType GetEnemyStats(Constants.EnemyType enemy)
    {
        return enemyTypes.Find(x => x.enemyType == enemy);
    }

    public void InstantiateArm(Constants.EnemyType enemy, Vector3 position)
    {

        //TODO:nici nu vreau sa comentez
        switch (enemy)
        {
            case Constants.EnemyType.Male:
            {
                GameObject arm = enemyLootArms.Find(x => x.name == "Knife");
                Instantiate(arm, position, Quaternion.identity);
                break;
            }
            case Constants.EnemyType.Pistol:
            {
                GameObject arm = enemyLootArms.Find(x => x.name == "pistol 1");
                Instantiate(arm, position, Quaternion.identity);
                break;
            }
            case Constants.EnemyType.AKA47:
            {
                GameObject arm = enemyLootArms.Find(x => x.name == "AKA47");
                Instantiate(arm, position, Quaternion.identity);
                break;
            }

            case Constants.EnemyType.Sniper:
            {
                GameObject arm = enemyLootArms.Find(x => x.name == "AKA47");
                Instantiate(arm, position, Quaternion.identity);
                break;
            }


        }

    }


}
