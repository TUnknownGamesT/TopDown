using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalking : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Transform _enemyTransform;
    private Vector3 _oldPosition;
    
    private const float _animationMultiplier=30;
    
    private static readonly int frontWalking = Animator.StringToHash("frontWalking");
    private static readonly int sideWalking = Animator.StringToHash("sideWalking");
    
    public Rigidbody[] rigidbodies;
    // Start is called before the first frame update
    private void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        SetRagdollEnabled(false);
    }
    
    public void SetRagdollEnabled(bool isEnabled)
    {
        
        gameObject.GetComponent<CapsuleCollider>().enabled = !isEnabled;
        animator.enabled = !isEnabled;

        foreach (var rb in rigidbodies)
        {
            rb.isKinematic = !isEnabled;
            rb.detectCollisions = isEnabled;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
        Vector3 newToOld = _oldPosition - _enemyTransform.position;

        float directionValue = Vector3.Dot(_enemyTransform.forward, newToOld);
        animator.SetFloat(frontWalking, directionValue*_animationMultiplier);
        
        directionValue = Vector3.Dot(_enemyTransform.right, newToOld);
        animator.SetFloat(sideWalking, directionValue*_animationMultiplier);
        
        _oldPosition = _enemyTransform.position;
    }
}
