using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    
    public float radius;
    [Range(0, 360)] 
    public float angle;
    public LayerMask obstructionMask;
    public LayerMask targetMask;
    [HideInInspector]
    public bool canSeePlayer;

    private bool _alreadyInView;
    private CancellationTokenSource _cts;
    private EnemyBrain _enemyBrain;
    public GameObject _playerRef;

    private void Awake()
    {
        _enemyBrain = GetComponent<EnemyBrain>();
        _cts = new CancellationTokenSource();
    }
    


    private void Start()
    {
        _playerRef = GameManager.playerRef.gameObject;
        FiledOfViewCheck();
    }


    public void EnemyDeath()
    {
        _cts.Cancel();
    }
    
    private void FiledOfViewCheck()
    {
        UniTask.Void(async () =>
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.05f), cancellationToken: _cts.Token);

            Collider[] rangeChecks =
                Physics.OverlapSphere(transform.position, radius, targetMask, QueryTriggerInteraction.Collide);

            if (rangeChecks.Length != 0)
            {
                Transform target = rangeChecks[0].transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;
                
                if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);
                    canSeePlayer = !Physics.Raycast(transform.position, directionToTarget, distanceToTarget,
                        obstructionMask);
                    if (canSeePlayer && !_alreadyInView)
                    {
                        _enemyBrain.PlayerInView();
                        _alreadyInView = true;
                    }
                }
            }
            else if (canSeePlayer)
            {
                canSeePlayer = false;
                _enemyBrain.PlayerOutOfView();
                _alreadyInView = false;
            }

            FiledOfViewCheck();
        });
    }
}