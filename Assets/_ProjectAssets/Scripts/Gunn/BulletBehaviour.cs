using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletBehaviour : MonoBehaviour
{
    public int damage;
    
    private void Start()
    {
        Destroy(gameObject, 5f);
    }
}
