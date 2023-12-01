using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletBehaviour : MonoBehaviour
{
    public int damage;
    public GameObject wallHitEffect;
    
    private void Start()
    {
        Destroy(gameObject, 5f);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
            Instantiate(wallHitEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
