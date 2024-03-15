using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;


[RequireComponent(typeof(FieldOfView),typeof(SoundComponent))]
public abstract class EnemyArms : MonoBehaviour
{
    public Transform armSpawnPoint;
    
    
    private Constants.EnemyType enemyType;
    [Header("Shooting")] protected float timeBetweenShoots;
    protected int damage;
    [Range(0,0.3f)]
    public float spread;
    private int numberOfBulletsPerShoot = 1;
    [Header("Reloading")] 
    private float fireRate;
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
    private int totalAmunition;
    private float angleBeforeShoot;


    private float timeSinceLastShot;
    protected FieldOfView _fieldOfView;
    
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
        numberOfBulletsPerShoot = enemyType.numberOfBulletsPerShoot;
        totalAmunition = enemyType.totalAmmunition;
        angleBeforeShoot = enemyType.angleBeforeShoot;
        
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
                    for (int i = 0; i < numberOfBulletsPerShoot; i++)
                    {
                        float xSpread = UnityEngine.Random.Range(-spread, spread);
                        float YSpread = UnityEngine.Random.Range(-spread, spread);

                        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position,
                            bulletSpawnPoint.rotation);
                        bullet.GetComponent<BulletBehaviour>().damage= damage;
                        Rigidbody rb =bullet.GetComponent<Rigidbody>();
                        rb.AddRelativeForce((Vector3.forward +new Vector3(xSpread,YSpread,0) )* bulletSpeed, ForceMode.Impulse);
 
                    }
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
                        await Reload();
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
    
    protected virtual bool CanShoot() => !reloading && CheckAngle() && timeSinceLastShot > 1f / (fireRate / 60f);

    protected virtual bool CheckAngle()
    {
        Vector3 direction = GameManager.playerRef.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, direction);
        if (angle < angleBeforeShoot)
        {
            Debug.Log("Angle check true");
            return true;
        }

        Debug.Log("Angle check false");
        return false;
    }

    private async UniTask Reload()
    {
        GetComponent<EnemyAnimations>()?.myWeapon.StartReload();
        await UniTask.Delay(TimeSpan.FromSeconds(reloadTime));
        GetComponent<EnemyAnimations>()?.myWeapon.ReloadComplete();
        reloading = false;
        int difference = totalAmunition - magSize;
        if ( totalAmunition - difference >= 0)
        {
            currentAmo += difference;
            totalAmunition -= difference;
        }
        else
        {
            currentAmo = totalAmunition;
            totalAmunition = 0;
        }
    }

    public virtual void DropArm()
    {
        Destroy(armPrefab);
        EnemyInitiator.instance.InstantiateArm(enemyType,transform.position);
    }

}