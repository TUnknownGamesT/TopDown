using System;
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
        switch (GetComponent<EnemyArms>().enemyType)
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
        }
    }

    public void TakeDmg(int damage)
    {
        health-=damage;
        if (health <= 0)
        {
            _enemyBrain.Death();
            GetComponent<AnimationController>()?.Die();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        var animations = GetComponent<EnemyAnimations>();
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDmg(1);
            ContactPoint contact = collision.contacts[0];

            // Calculate the force direction based on the collision point
            Vector3 forceDirection = contact.point - transform.position;

            // Apply force to the ragdoll's rigidbody
            GetComponent<EnemyAnimations>().rigidbodies[5].AddForceAtPosition(forceDirection.normalized * forceMultiplier, contact.point);


        }
    }
}
