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


    [Header("Shooting")] protected float timeBetweenShoots;
    protected int damage;
    [Header("Reloading")] private float fireRate;
    private float reloadTime;
    private int magSize;
    private float bulletSpeed;
    private float currentAmo;
    private bool reloading;
    [Header("References")] private Transform bulletSpawnPoint;
    private GameObject bulletPrefab;


    private float timeSinceLastShot;
    protected FieldOfView _fieldOfView;


    protected virtual bool CanShoot() => !reloading && timeSinceLastShot > 1f / (fireRate / 60f);

    private void Awake()
    {
        _fieldOfView = GetComponent<FieldOfView>();
    }


    protected void Start()
    {
        InitStats(EnemyInitiator.instance.GetEnemyStats(enemyType));
    }

    protected void Update()
    {
        timeSinceLastShot += Time.deltaTime;
    }


    protected virtual void InitStats(EnemyType enemyType)
    {
        damage = enemyType.damage;
        fireRate = enemyType.fireRate;
        reloadTime = enemyType.reloadTime;
        magSize = enemyType.magSize;
        bulletSpeed = enemyType.bulletSpeed;
        currentAmo = enemyType.magSize;
        timeBetweenShoots = enemyType.timeBetweenShoots;

        bulletPrefab = enemyType.bulletPrefab;
        GameObject armPrefab =
            Instantiate(enemyType.armPrefab, armSpawnPoint.position, armSpawnPoint.localRotation, transform);
        bulletSpawnPoint = armPrefab.transform.GetChild(0);
    }


    public virtual void Shoot()
    {
        UniTask.Void(async () =>
        {
            try
            {
                if (currentAmo > 0 && CanShoot())
                {
                    Rigidbody rb = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation)
                        .GetComponent<Rigidbody>();
                    rb.AddRelativeForce(Vector3.forward * bulletSpeed, ForceMode.Impulse);

                    currentAmo--;
                    timeSinceLastShot = 0;

                    await UniTask.Delay(TimeSpan.FromSeconds(timeBetweenShoots));
                }
                else if (currentAmo <= 0)
                {
                    if (!reloading)
                    {
                        reloading = true;
                        Reload();
                    }
                }

                if (_fieldOfView.canSeePlayer)
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
                    Shoot();
                }
            }
            catch (Exception e)
            {
                Debug.Log("Carpeala moticel nu te uita", this);
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