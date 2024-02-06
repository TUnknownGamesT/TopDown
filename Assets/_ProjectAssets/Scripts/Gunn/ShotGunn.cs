using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunn : Gunn
{
    public float numberOfBulletsPerShoot;
    public MeshRenderer meshRenderer;

    public override void Shoot()
    {
        if (CurrentAmunition > 0 && CanShoot())
        {
            
             _armHandler.animation.Shoot();
            for (int i = 0; i < numberOfBulletsPerShoot; i++)
            {
                float xSpread = UnityEngine.Random.Range(-spread, spread);
                float YSpread = UnityEngine.Random.Range(-spread, spread);
                
                Rigidbody rb = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation)
                    .GetComponent<Rigidbody>();
                rb.AddRelativeForce((Vector3.forward + new Vector3(xSpread, YSpread, 0)) * bulletSpeed,
                    ForceMode.Impulse);
            }

            vfx.Play();
            CurrentAmunition--;
            TimeSinceLasrShot = 0;
            CameraController.ShakeCamera();
            onShoot?.Invoke();
            _soundComponent.PlaySound(shootSound);
            _armHandler.animation.StopShooting();
        }
        else if (CurrentAmunition <= 0 && totalAmunition > 0)
        {
            if (!reloading)
            {
                Reload();
            }
        }
    }
    
    public override void HighLight()
    {
        base.HighLight();
        meshRenderer.material = Constants.instance.highLightInteractable;
    }
    
    public override void UnHighLight()
    {
        base.UnHighLight();
        meshRenderer.material = Constants.instance.unhighlightInteractable;
    }
}