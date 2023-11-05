using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

[RequireComponent(typeof(FieldOfView))]
public abstract class EnemyArms : MonoBehaviour
{
    public Transform armSpawnPoint;
    public Constants.EnemyType enemyType;


    [Header("Shooting")] 
    private float timeBetweenShoots;
    private float damage;
    [Header("Reloading")]
    private float fireRate;
    private float reloadTime;
    private int magSize;
    private float bulletSpeed;
    private float currentAmo;
    private bool reloading;
    [Header("References")]
    private Transform bulletSpawnPoint;
    private GameObject bulletPrefab;
    
    
    private float timeSinceLastShot;
    private FieldOfView _fieldOfView;


    private bool CanShoot() => !reloading && timeSinceLastShot > 1f / (fireRate / 60f);

    private void Awake()
    {
        _fieldOfView = GetComponent<FieldOfView>();
    }

    private void OnEnable()
    {
        FieldOfView.onPlayerInView += Shoot;
    }
    
    private void OnDisable()
    {
        FieldOfView.onPlayerInView -= Shoot;
    }
    
  
    protected void Start()
    {
       InitStats(EnemyInitiator.instance.GetEnemyStats(enemyType));
    }

    protected void Update()
    {
        timeSinceLastShot += Time.deltaTime;
    }


    private void InitStats(EnemyType enemyType)
    {
        damage = enemyType.damage;
        fireRate = enemyType.fireRate;
        reloadTime = enemyType.reloadTime;
        magSize = enemyType.magSize;
        bulletSpeed = enemyType.bulletSpeed;
        currentAmo = enemyType.magSize;
        timeBetweenShoots = enemyType.timeBetweenShoots;
        
        bulletPrefab = enemyType.bulletPrefab;
        GameObject armPrefab = Instantiate(enemyType.armPrefab, armSpawnPoint.position, armSpawnPoint.rotation,transform);
        bulletSpawnPoint = armPrefab.transform.GetChild(0);
    }
    

    private void Shoot()
    {

        UniTask.Void(async () =>
        {
            if(currentAmo>0&& CanShoot())
            {
                Debug.Log(bulletSpawnPoint.position);
                Rigidbody rb =  Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation).GetComponent<Rigidbody>();
                rb.AddRelativeForce(Vector3.forward * bulletSpeed, ForceMode.Impulse);

                currentAmo--;
                timeSinceLastShot = 0;
            }
            else if(currentAmo<=0)
            {
                if (!reloading)
                {
                    reloading = true;
                    Reload();
                }
            }
            
            await UniTask.Delay(TimeSpan.FromSeconds(timeBetweenShoots));
            
            if (_fieldOfView.canSeePlayer)
            {
                Shoot();
            }
        });
        
    }
    
    private void Reload()
    {
        UniTask.Void(async () =>
        {
            await UniTask.Delay(TimeSpan.FromSeconds(reloadTime));
            reloading = false;
            currentAmo = magSize;
        });
    }

}
