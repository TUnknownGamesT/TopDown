using System;
using System.Collections;
using System.Collections.Generic;
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

    public EnemyType GetEnemyStats(Constants.EnemyType enemy)
    {
        return enemyTypes.Find(x => x.enemyType == enemy);
    }
}
