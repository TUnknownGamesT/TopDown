using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Gunn : MonoBehaviour
{

    [Header("Shooting")]
    public float damage;
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
    
    
    private float timeSinceLasrShot;


    private bool CanShoot() => !reloading && timeSinceLasrShot > 1f / (fireRate / 60f);
    
    protected void OnDisable()
    {
        UserInputController._leftClick.performed -= Shoot;
    }


    protected void Start()
    {
        UserInputController._leftClick.performed += Shoot;
    }


    protected void Update()
    {
        timeSinceLasrShot += Time.deltaTime;
    }

    protected void Shoot(InputAction.CallbackContext obj)
    {

        if(currentAmo>0&& CanShoot())
        {
            Rigidbody rb =  Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation).GetComponent<Rigidbody>();
            rb.AddRelativeForce(Vector3.forward * bulletSpeed, ForceMode.Impulse);

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
}
