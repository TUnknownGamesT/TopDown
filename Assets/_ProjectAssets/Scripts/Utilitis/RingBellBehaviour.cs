using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingBellBehaviour : MonoBehaviour
{
    public CustomBoxCollider customBoxCollider;
    public Transform destination;
    public DoorBehaviour doorBehaviour;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            customBoxCollider.NotticeEenemies(destination);
        }
    }
}
