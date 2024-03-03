
using System;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;

public class DoorBehaviour : Destroyable
{
    private void Awake()
    {
        peacesMass = Constants.doorPeacesMass;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Wall") && !other.gameObject.CompareTag("Ground"))
        {
            impulseforce = 20;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            DesTroy(other.transform.forward);   
        }
        
    }
}
