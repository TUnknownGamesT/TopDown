using System;
using System.Security.Permissions;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    public Transform[] _travelPoints;
    public GameObject playerRef;
    public float stoppingDistance;
    public float damping;
    
    private NavMeshAgent _navMeshAgent;
    private CancellationTokenSource _cts;
    private bool staringAtPlayer = false;

    private void Awake()
    {
        _cts = new CancellationTokenSource();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }


    private void OnEnable()
    {
        FieldOfView.onPlayerInView += PlayerInView;
        FieldOfView.onPlayerOutOfView += PlayerOutOfView;
    }
    
    private void OnDisable()
    {
        FieldOfView.onPlayerInView -= PlayerInView;
        FieldOfView.onPlayerOutOfView -= PlayerOutOfView;
    }


    private void Start()
    {
        Travel();
    }


    private void Update()
    {
        if(staringAtPlayer)
            StartingAtPlayer();
    }

    private void Travel()
    {
        UniTask.Void(async () =>
        {
            // _navMeshAgent.Move(_travelPoints[Random.Range(0,_travelPoints.Length)].position);
             _navMeshAgent.destination = _travelPoints[Random.Range(0,_travelPoints.Length)].position;

             await UniTask.WaitUntil(()=>_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance, cancellationToken: _cts.Token);
             
             Travel();
        });
    }

    private void PlayerOutOfView()
    {
        _navMeshAgent.isStopped = false;
        _cts.Cancel();
        _cts = new CancellationTokenSource();
        Travel();
    }
    
    private void PlayerInView()
    {
        _cts.Cancel();
        _cts = new CancellationTokenSource();
        FollowPlayer();
    }


    private void FollowPlayer()
    {
        UniTask.Void(async  () =>
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f), cancellationToken: _cts.Token);
            if (Vector3.Distance(transform.position, playerRef.transform.position) > stoppingDistance)
            {
                staringAtPlayer= false;
                _navMeshAgent.destination = playerRef.transform.position;
            }
            else
            {
                staringAtPlayer = true;
            }
            
            FollowPlayer();
        });
    }

    private void StartingAtPlayer()
    {
        var lookPos = playerRef.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation,Time.deltaTime * damping);
    }
    
    


    
}
