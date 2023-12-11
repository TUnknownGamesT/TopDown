using UnityEngine;


[CreateAssetMenu(fileName = "EnemyType", menuName = "ScriptableObjects/EnemyType", order = 1)]
public class EnemyType : ScriptableObject
{
    public GameObject armPrefab;
    public Constants.EnemyType enemyType;
    [Header("Shooting")]
    public float timeBetweenShoots;
    public int damage;
    [Header("Reloading")]
    public float fireRate;
    public float reloadTime;
    public int magSize;
    public float totalAmmunition;
    public float bulletSpeed;
    public bool reloading;
    [Header("References")]
    public GameObject bulletPrefab;
    public float stoppingDistance;


}
