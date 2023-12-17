using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;


[RequireComponent(typeof(SoundComponent))]
public class Expliosible : MonoBehaviour
{

    public GameObject explosionVFX;

    
    [Header("Explosion")]
    public float explosionForce = 10f; 
    public float explosionRadius = 5f;
    public int dmg = 10;
    public AudioClip explosionSound;
    
    private SoundComponent soundComponent;


    private void Awake()
    {
        soundComponent = GetComponent<SoundComponent>();
    }


    private void PlayVFX()
    {
        Instantiate(explosionVFX, transform.position, Quaternion.identity);
    }

    void OnDrawGizmosSelected()
    {
        // Draw a wire sphere to visualize the explosion radius in the scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
    
    
    private void FindRigidBodies()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider collider in colliders)
        {
            var player = collider.GetComponent<PlayerHealth>();
            var enemy = collider.GetComponent<EnemyHealth>();
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (player!=null)
            {
                player.TakeDamage(dmg);
                if (player.health <= 0)
                {
                    var playerAnimation = collider.GetComponent<PlayerAnimation>();
                    foreach (var element in playerAnimation.rigidbodies)
                    {
                        ApplyForce(element);
                    }
                }
            }else
            if (enemy!=null)
            {
                enemy.TakeDmg(dmg);
                if (enemy.health <= 0)
                {
                    var animator = collider.GetComponent<AnimationController>();
                    foreach (var element in animator.rigidbodies)
                    {
                       ApplyForce(element);
                    }
                }
            }else if (rb != null)
            {
                ApplyForce(rb);
            }
        }
    }

    //TODO: create animator class to derivate it to player and enemy, then make apply force for all bones here
    private void ApplyForceRagDoll()
    {
        
    }

    private void ApplyForce(Rigidbody rb)
    {
        // Apply force to the rigidbody based on distance from the explosion point
        Vector3 direction = rb.gameObject.transform.position - transform.position;
        float distance = direction.magnitude;

        // Calculate the force to be applied using an inverse square law
        float force = (1 - distance / explosionRadius) * explosionForce;

        // Apply the force
        rb.AddForce(direction.normalized * force, ForceMode.Impulse);
        
        UniTask.Void(async () =>
        {
            await UniTask.Delay(TimeSpan.FromSeconds(2));
            Destroy(gameObject);
        });

    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag =="Bullet")
        {
            soundComponent.PlaySound(explosionSound);
            PlayVFX();
            CameraShake.ExplosionCameraShake();
            FindRigidBodies();
        }
    }
}
