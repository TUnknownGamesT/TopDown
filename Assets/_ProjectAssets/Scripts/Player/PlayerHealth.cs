using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public static Action<int> onPlayerGetDamage;
    public static Action<int> onPlayerGetHeal;
    
    public float currentHealth;

    private float maxLife;
    
    private void Start()
    {
        UIManager.instance.SetHealthBarMaxLife(currentHealth);
        maxLife = currentHealth;
    }
    
    
    

    public void TakeDamage(int damageReceived)
    {
        currentHealth -= damageReceived;
        CameraController.instance.TakeDamageEffect();
        onPlayerGetDamage?.Invoke(damageReceived);
        if (currentHealth<=0)
        {
            Cursor.visible = true;
            Die();
        }
    }

    public void IncreaseLife(int amount)
    {
        if (currentHealth + amount > maxLife)
        {
            currentHealth = maxLife;
        }
        else
        {
            currentHealth += amount;
        }

        onPlayerGetHeal?.Invoke(amount);
    }
    
    
    private void Die()
    {
        GetComponent<PlayerAnimation>()?.Die();
        GetComponent<PlayerMovement>()?.Die();
        GetComponent<PlayerRotation>()?.Die();
        GetComponent<PlayerArmHandler>()?.Die();
        UIManager.instance.Die();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            int damageReceived = collision.gameObject.GetComponent<BulletBehaviour>().damage;
            TakeDamage(damageReceived);
        }
    }
}
