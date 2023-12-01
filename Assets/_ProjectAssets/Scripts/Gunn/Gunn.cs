using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Gunn : MonoBehaviour,IInteractable
{

    [Header("Shooting")]
    public float damage;
    public ParticleSystem vfx;
    [Header("Reloading")]
    public float fireRate;
    public float reloadTime;
    public int magSize;
    public float bulletSpeed;
    public float currentAmo;
    public bool reloading;
    [Header("References")]
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;


    public Event animation = new Event();
    
    private float timeSinceLasrShot;
    private MeshRenderer _renderer;


    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }


    protected void Update()
    {
        timeSinceLasrShot += Time.deltaTime;
    }

    private bool CanShoot() => !reloading && timeSinceLasrShot > 1f / (fireRate / 60f);
    
    public void Shoot()
    {

        if(currentAmo>0&& CanShoot())
        {
            Rigidbody rb =  Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation).GetComponent<Rigidbody>();
            rb.AddRelativeForce(Vector3.forward * bulletSpeed, ForceMode.Impulse);
            vfx.Play();
            currentAmo--;
            timeSinceLasrShot = 0;
        }
        else if(currentAmo<=0)
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
            currentAmo = magSize;
        });
    }

    public void Interact()
    {
        UnHighLight();
        PlayerArmHandler.instance.ChangeArm(gameObject);
    }

    public void HighLight()
    {
        _renderer.material = Constants.instance.gunnHighLight;
    }

    public void UnHighLight()
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
