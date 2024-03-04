using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    public int healthPoints;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().IncreaseLife(healthPoints);
            Destroy(gameObject);
        }
    }
}
