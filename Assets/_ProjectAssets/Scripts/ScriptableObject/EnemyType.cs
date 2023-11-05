using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyType", menuName = "ScriptableObjects/EnemyType", order = 1)]
public class EnemyType : ScriptableObject
{
    public GameObject armPrefab;
    public Constants.EnemyType enemyType;
    [Header("Shooting")]
    public float timeBetweenShoots;
    public float damage;
    [Header("Reloading")]
    public float fireRate;
    public float reloadTime;
    public int magSize;
    public float bulletSpeed;
    public float currentAmo;
    public bool reloading;
    [Header("References")]
    public GameObject bulletPrefab;
    
}
