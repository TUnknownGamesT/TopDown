using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletBehaviour : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 5f);
    }
}
