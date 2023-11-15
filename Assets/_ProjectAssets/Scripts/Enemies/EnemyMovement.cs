using System;
using System.Security.Permissions;
using System.Threading;
using Cysharp.Threading.Tasks;
using MoreMountains.FeedbacksForThirdParty;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    public Transform[] _travelPoints;
    public float stoppingDistance;

    private NavMeshAgent _navMeshAgent;
    private CancellationTokenSource _cts;

    private void Awake()
    {
        _cts = new CancellationTokenSource();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }
    

    private void Start()
    {
        if(_travelPoints.Length > 0)
            Travel();
    }
    
    public void PlayerOutOfView()
    {
        _navMeshAgent.isStopped = false;
        _cts.Cancel();
        _cts = new CancellationTokenSource();
        if(_travelPoints.Length > 0)
            Travel();
    }

    public void PlayerInView()
    {
        _cts.Cancel();
        _cts = new CancellationTokenSource();
        FollowPlayer();
    }


    public void EnemyDeath()
    {
        _cts.Cancel();
    }
    
    private void Travel()
    {
        UniTask.Void(async () =>
        {
            _navMeshAgent.destination = _travelPoints[Random.Range(0,_travelPoints.Length)].position;

            await UniTask.WaitUntil(()=>_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance, cancellationToken: _cts.Token);
             
            Travel();
        });
    }


    private void FollowPlayer()
    {
        UniTask.Void(async  () =>
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f), cancellationToken: _cts.Token);
            if (Vector3.Distance(transform.position, GameManager.playerRef.position) > stoppingDistance)
            {
                Debug.Log("FollowPlayer");
                _navMeshAgent.destination = GameManager.playerRef.position;
            }
            
            FollowPlayer();
        });
    }





}
