using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SoundComponent))]
public abstract class Gunn : MonoBehaviour,IInteractable
{
    
    
    public static Action onShoot;
    public static Action<int> onReload;
    public static Action<int,int> onPickUpNewWeapon;


    public Constants.EnemyType enemyDrop;
    [Header("Shooting")]
    public float damage;
    public ParticleSystem vfx;
    [Range(0,1f)]
    public float spread;
    [Header("Reloading")]
    public float fireRate;
    public float reloadTime;
    public int magSize;
    public int totalAmunition;
    public float bulletSpeed;
    public bool reloading;
    [Header("References")]
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
  
    public AudioClip shootSound;

    public Event animation = new Event();

    protected SoundComponent _soundComponent;
    protected float TimeSinceLasrShot;
    private MeshRenderer _renderer;
    protected int CurrentAmunition;

    protected PlayerArmHandler _armHandler;

    private void Awake()
    {
        CurrentAmunition = magSize;
        _renderer = GetComponent<MeshRenderer>();
        _soundComponent = GetComponent<SoundComponent>();
        
    }

    public void SetArmHandler(PlayerArmHandler arm)
    {
        _armHandler = arm;
    } 

    protected void Update()
    {
        TimeSinceLasrShot += Time.deltaTime;
    }

    protected virtual bool CanShoot() => !reloading && TimeSinceLasrShot > 1f / (fireRate / 60f);
    
    public virtual void Shoot()
    {

        if(CurrentAmunition>0&& CanShoot())
        {
            
            float xSpread = UnityEngine.Random.Range(-spread, spread);
            float YSpread = UnityEngine.Random.Range(-spread, spread);
            
            Debug.Log("shoot");
            _armHandler.animation.Shoot();
            Rigidbody rb =  Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation).GetComponent<Rigidbody>();
            rb.AddRelativeForce((Vector3.forward + new Vector3(xSpread,YSpread,0)) * bulletSpeed, ForceMode.Impulse);
            vfx.Play();
            CurrentAmunition--;
            TimeSinceLasrShot = 0;
            CameraController.ShakeCamera();
            onShoot?.Invoke();
            _soundComponent.PlaySound(shootSound);
            _armHandler.animation.StopShooting();
        }
        else if(CurrentAmunition<=0 && totalAmunition>0)
        {
            if (!reloading)
            {
                Reload();
            }
        }
    }

    public void Reload()
    {
        reloading = true;
        _armHandler.animation.Reload();
        UniTask.Void(async () =>
        {
            await UniTask.Delay(TimeSpan.FromSeconds(reloadTime));
            reloading = false;
            _armHandler.animation.ReloadComplete();
            
            int difference = magSize - CurrentAmunition;
            if ( totalAmunition - difference >= 0)
            {
                CurrentAmunition += difference;
                totalAmunition -= difference;
            }
            else
            {
                CurrentAmunition = totalAmunition;
                totalAmunition = 0;
            }
            
            onPickUpNewWeapon?.Invoke(CurrentAmunition,totalAmunition);
        });
    }

    public void QuickPressInteract()
    {
        UnHighLight();
        PlayerArmHandler.instance.ChangeArm(gameObject);
        CurrentAmunition = magSize;
        onPickUpNewWeapon?.Invoke(CurrentAmunition,totalAmunition);
    }

    public virtual void HighLight()
    {
        _renderer.material = Constants.instance.highLightInteractable;
    }

    public virtual void UnHighLight()
    {
        _renderer.material = Constants.instance.unhighlightInteractable;
    }


    private void OnTriggerEnter(Collider other)
    {
        HighLight();
    }

    private void OnTriggerExit(Collider other)
    {
        UnHighLight();
    }
}
