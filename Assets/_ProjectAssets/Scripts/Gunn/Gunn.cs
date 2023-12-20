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

    [Header("Shooting")]
    public float damage;
    public ParticleSystem vfx;
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
    public SoundComponent soundComponent;
    public AudioClip shootSound;

    public Event animation = new Event();
    
    protected float timeSinceLasrShot;
    private MeshRenderer _renderer;
    private int currentAmunition;

    private void Awake()
    {
        currentAmunition = magSize;
        _renderer = GetComponent<MeshRenderer>();
        soundComponent = GetComponent<SoundComponent>();
        
    }

    protected void Update()
    {
        timeSinceLasrShot += Time.deltaTime;
    }

    protected virtual bool CanShoot() => !reloading && timeSinceLasrShot > 1f / (fireRate / 60f);
    
    public void Shoot()
    {

        if(currentAmunition>0&& CanShoot())
        {
            Rigidbody rb =  Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation).GetComponent<Rigidbody>();
            rb.AddRelativeForce(Vector3.forward * bulletSpeed, ForceMode.Impulse);
            vfx.Play();
            currentAmunition--;
            timeSinceLasrShot = 0;
            CameraShake.ShakeCamera();
            onShoot?.Invoke();
            soundComponent.PlaySound(shootSound);
        }
        else if(currentAmunition<=0 && totalAmunition>0)
        {
            if (!reloading)
            {
                reloading = true;
                Reload();
            }
        }
    }

    private void Reload()
    {
        UniTask.Void(async () =>
        {
            await UniTask.Delay(TimeSpan.FromSeconds(reloadTime));
            reloading = false;
            if (totalAmunition - magSize >= 0)
            {
                currentAmunition= magSize;
                totalAmunition -= magSize;
            }
            else
            {
                currentAmunition = totalAmunition;
                totalAmunition = 0;
            }
            
            onPickUpNewWeapon?.Invoke(currentAmunition,totalAmunition);
        });
    }

    public void Interact()
    {
        UnHighLight();
        PlayerArmHandler.instance.ChangeArm(gameObject);
        currentAmunition = magSize;
        onPickUpNewWeapon?.Invoke(currentAmunition,totalAmunition);
    }

    public virtual void HighLight()
    {
        _renderer.material = Constants.instance.gunnHighLight;
    }

    public virtual void UnHighLight()
    {
        _renderer.material = Constants.instance.gunnUnHighLight;
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
