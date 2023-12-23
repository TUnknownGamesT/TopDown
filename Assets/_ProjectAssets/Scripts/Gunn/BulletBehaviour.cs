using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletBehaviour : MonoBehaviour
{
    public int damage;
    public GameObject wallHitEffect;
    public GameObject bloodEffect;
    
    private void Start()
    {
        Destroy(gameObject, 5f);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Quaternion rot = Quaternion.FromToRotation(Vector3.back, collision.contacts[0].normal);
            Instantiate(wallHitEffect, collision.contacts[0].point, rot);
        }else if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player"))
        {
            GameObject blood = Instantiate(bloodEffect, collision.contacts[0].point, Quaternion.LookRotation(transform.forward));
        }
        Destroy(gameObject);
    }
}
