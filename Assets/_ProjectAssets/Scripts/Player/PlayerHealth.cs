using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public static Action<int> onPlayerGetDamage;
    
    public float health;
    
    
    private void Start()
    {
        UIManager.instance.SetHealthBarMaxLife(health);
    }
    
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            int damageReceived = collision.gameObject.GetComponent<BulletBehaviour>().damage;
            health -= damageReceived;
            onPlayerGetDamage?.Invoke(damageReceived);
        }
    }

    public void TakeDamage(int damageReceived)
    {
        health -= damageReceived;
        onPlayerGetDamage?.Invoke(damageReceived);
    }
}
