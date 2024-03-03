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
            TakeDamage(damageReceived);
        }
    }

    public void TakeDamage(int damageReceived)
    {
        health -= damageReceived;
        CameraController.instance.TakeDamageEffect();
        onPlayerGetDamage?.Invoke(damageReceived);
        if (health<=0)
        {
            Cursor.visible = true;
            Die();
        }
    }
    
    
    
    private void Die()
    {
        GetComponent<PlayerAnimation>()?.Die();
        GetComponent<PlayerMovement>()?.Die();
        GetComponent<PlayerRotation>()?.Die();
        GetComponent<PlayerArmHandler>()?.Die();
        UIManager.instance.Die();
    }
}
