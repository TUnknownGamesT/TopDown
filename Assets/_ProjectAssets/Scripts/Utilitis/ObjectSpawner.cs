using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    
    public GameObject _objectToSpawn;
    public float delay;
    public string tag;

    private bool alreadyStarted;
    

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(tag)&& !alreadyStarted)
        {
            alreadyStarted = true;
            UniTask.Void(async () =>
            {
                await UniTask.Delay(TimeSpan.FromSeconds(delay));
                Instantiate(_objectToSpawn, transform.position, Quaternion.identity);
                alreadyStarted = false;
            });
        }
        
    }
}
