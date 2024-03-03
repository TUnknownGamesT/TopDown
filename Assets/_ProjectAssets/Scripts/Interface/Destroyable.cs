using System;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Destroyable:MonoBehaviour
{
    protected float peacesMass;
    
    private bool alreadyDestroyd;
    protected float impulseforce=100;

    public void DesTroy(Vector3 direction)
    {
        foreach (Transform children in transform)
        {
            MeshCollider meshCollider= children.AddComponent<MeshCollider>();
            meshCollider.convex = true;
            Rigidbody _rb =  children.AddComponent<Rigidbody>();
            _rb.mass = peacesMass;
            _rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            _rb.AddForce(direction.normalized * impulseforce, ForceMode.Impulse);
        }

        foreach (Transform children in transform)
        {
            children.SetParent(null);
        }

        UniTask.Void(async () =>
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.3f));
            gameObject.GetComponent<BoxCollider>().enabled = false;
        });
    }
}
