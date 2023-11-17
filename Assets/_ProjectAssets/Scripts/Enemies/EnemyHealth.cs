using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    
    private float health;
    private EnemyBrain _enemyBrain;

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
            case Constants.EnemyType.Ak47:
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health--;
            if (health <= 0)
            {
                _enemyBrain.Death();
                Destroy(gameObject);
            }
        }
    }
}
