using System;
using Cinemachine;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    
    public float health;
    private EnemyBrain _enemyBrain;
    public float forceMultiplier;

    private void Awake()
    {
        _enemyBrain = GetComponent<EnemyBrain>();
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        switch (_enemyBrain.enemyType)
        {
            case Constants.EnemyType.Pistol:
            {
                health = Constants.PistolEnemyHealth;
                break;    
            }
            case Constants.EnemyType.AKA47:
            {
                health = Constants.Ak47EnemyHealth;
                break;
            }

            case Constants.EnemyType.Male:
            {
                health = Constants.MaleEnemyHealth;
                break;
            }
            case Constants.EnemyType.Dracu:
            {
                health = Constants.DracuEnemyHealth;
                break;
            }
        }
    }

    public void TakeDmg(int damage)
    {
        health-=damage;
        if (health <= 0)
        {
            CameraController.instance.KillEffect();
            _enemyBrain.Death();
            GetComponent<AnimationController>()?.Die();
            Debug.Log("death");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDmg(1);
            if (health<=0)
            {
                ContactPoint contact = collision.contacts[0];
                foreach (var rb in GetComponent<AnimationController>().rigidbodies)
                {
                    Vector3 direction = contact.point - transform.position;
                    direction.y = 0;
                    float distance = direction.magnitude;

                    // Calculate the force to be applied using an inverse square law
                    float force = (2 - distance) * forceMultiplier;
                    if (force<0)
                    {
                        force = 0;
                    }
                    else
                    {
                        force = -force;
                    }

                    // Apply the force
                    rb.AddForce(direction.normalized * force, ForceMode.Impulse);
                    Debug.Log(force +"force");
                }
            }
            

        }
    }
}
