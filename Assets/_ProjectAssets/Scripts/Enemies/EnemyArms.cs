using System;
using Cysharp.Threading.Tasks;
using UnityEngine;


[RequireComponent(typeof(FieldOfView),typeof(SoundComponent))]
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
    private GameObject armPrefab;
    private SoundComponent soundComponent;
    private AudioClip shootSound;


    private float timeSinceLastShot;
    protected FieldOfView _fieldOfView;


    protected virtual bool CanShoot() => !reloading && timeSinceLastShot > 1f / (fireRate / 60f);

    public virtual void Awake()
    {
        soundComponent = GetComponent<SoundComponent>();
        _fieldOfView = GetComponent<FieldOfView>();
    }

    protected void Update()
    {
        timeSinceLastShot += Time.deltaTime;
    }


    public virtual void InitStats(EnemyType enemyType)
    {
        this.enemyType = enemyType.enemyType;
        damage = enemyType.damage;
        fireRate = enemyType.fireRate;
        reloadTime = enemyType.reloadTime;
        magSize = enemyType.magSize;
        bulletSpeed = enemyType.bulletSpeed;
        currentAmo = enemyType.magSize;
        timeBetweenShoots = enemyType.timeBetweenShoots;
        bulletPrefab = enemyType.bulletPrefab;
        shootSound = enemyType.shootSound;
        
        armPrefab = Instantiate(enemyType.armPrefab, armSpawnPoint.position, Quaternion.identity, armSpawnPoint.transform);
        armPrefab.transform.localPosition = Vector3.zero;
        armPrefab.transform.localRotation = Quaternion.identity;
        
        if(enemyType.enemyType !=  Constants.EnemyType.Male)
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
                    soundComponent.PlaySound(shootSound);

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

    public void DropArm()
    {
        Destroy(armPrefab);
        EnemyInitiator.instance.InstantiateArm(enemyType,transform.position);
    }

}