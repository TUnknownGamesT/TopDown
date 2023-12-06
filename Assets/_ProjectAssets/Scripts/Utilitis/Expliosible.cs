using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expliosible : MonoBehaviour
{

    public List<ParticleSystem> vfxList = new List<ParticleSystem>();

    
    [Header("Explosion")]
    public float explosionForce = 10f; 
    public float explosionRadius = 5f; 

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag =="Bullet")
        {
           PlayVFX();
            CameraShake.ExplosionCameraShake();
            ApplyForce();
        }
    }
    
    private void PlayVFX()
    {
        foreach (ParticleSystem vfx in vfxList)
        {
            vfx.Play();
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a wire sphere to visualize the explosion radius in the scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
    
    private void ApplyForce()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider collider in colliders)
        {
            var player = collider.GetComponent<PlayerAnimation>();
            var enemy = collider.GetComponent<EnemyWalking>();
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (player!=null)
            {
                player.Explode();
                foreach (var element in player.rigidbodies)
                {
                    // Apply force to the rigidbody based on distance from the explosion point
                    Vector3 direction = collider.transform.position - transform.position;
                    float distance = direction.magnitude;

                    // Calculate the force to be applied using an inverse square law
                    float force = (1 - distance / explosionRadius) * explosionForce;

                    // Apply the force
                    element.AddForce(direction.normalized * force, ForceMode.Impulse);
                }
            }else
            if (enemy!=null)
            {
                enemy.Explode();
                foreach (var element in enemy.rigidbodies)
                {
                    // Apply force to the rigidbody based on distance from the explosion point
                    Vector3 direction = collider.transform.position - transform.position;
                    float distance = direction.magnitude;

                    // Calculate the force to be applied using an inverse square law
                    float force = (1 - distance / explosionRadius) * explosionForce;

                    // Apply the force
                    element.AddForce(direction.normalized * force, ForceMode.Impulse);
                }
            }else if (rb != null)
            {
                // Apply force to the rigidbody based on distance from the explosion point
                Vector3 direction = collider.transform.position - transform.position;
                float distance = direction.magnitude;

                // Calculate the force to be applied using an inverse square law
                float force = (1 - distance / explosionRadius) * explosionForce;

                // Apply the force
                rb.AddForce(direction.normalized * force, ForceMode.Impulse);
            }
        }
    }
}
