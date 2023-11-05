using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{

    public static Action onPlayerInView;
    public static Action onPlayerOutOfView;
    
    
    public float radius;
    [Range(0,360)]
    public float angle;

    public GameObject playerRef;
    public LayerMask obstructionMask;
    public LayerMask targetMask;

    public bool canSeePlayer;
    
    private bool alreadyInView = false;


    private void Start()
    {
        FiledOfViewCheck();
    }
    
    private void FiledOfViewCheck()
    {
        
        UniTask.Void(async () =>
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.05f));
            
            Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius,targetMask,QueryTriggerInteraction.Collide);

            if (rangeChecks.Length != 0)
            {
                Transform target = rangeChecks[0].transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;
            
                if(Vector3.Angle(transform.forward,directionToTarget)<angle/2)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);
                    canSeePlayer = !Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask);
                    if (canSeePlayer && !alreadyInView)
                    {
                        onPlayerInView?.Invoke();
                        alreadyInView = true;
                    }
                }
            
            }else if (canSeePlayer)
            {
                canSeePlayer = false;
                onPlayerOutOfView?.Invoke();
                alreadyInView= false;
            }
            FiledOfViewCheck();
        });
       
    }
}
