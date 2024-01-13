using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    public List<Transform> _travelPoints;
    public float stoppingDistance;
    public float pauseBetweenMovement;
    public EnemyBrain enemyBrain;
    
    private NavMeshAgent _navMeshAgent;
    private CancellationTokenSource _cts;

    private void Awake()
    {
        enemyBrain = GetComponent<EnemyBrain>();
        _cts = new CancellationTokenSource();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }
    

    private void Start()
    {
        if (_travelPoints.Count > 0)
        {
            Travel();
        }
        else
        {
            enemyBrain.FinishMoving();
        }
    }

    public void PlayerOutOfView()
    {
        _cts.Cancel();
        _navMeshAgent.SetDestination(transform.position);
        _cts = new CancellationTokenSource();
        _travelPoints.Add(GameManager.playerRef);
        Travel();
    }

    public void PlayerInView()
    {
        _cts.Cancel();
        _cts = new CancellationTokenSource();
        if(_travelPoints.Contains(GameManager.playerRef))
            _travelPoints.Remove(GameManager.playerRef);
        FollowPlayer();
    }


    public void EnemyDeath()
    {
        _cts.Cancel();
        _navMeshAgent.destination = transform.position;
    }
    
    public void Travel()
    {
        UniTask.Void(async () =>
        {
            _navMeshAgent.destination = _travelPoints[Random.Range(0,_travelPoints.Count)].position;

            Debug.Log("START MOVEMENT");
            
            await UniTask.WaitUntil(()=>Vector3.Distance(transform.position,_navMeshAgent.destination) <= _navMeshAgent.stoppingDistance, cancellationToken: _cts.Token);
            
            Debug.Log("Finish Movement");
            
            enemyBrain.FinishMoving();

            Debug.Log("Start Looking Around");
            
            await UniTask.Delay(TimeSpan.FromSeconds(pauseBetweenMovement), cancellationToken: _cts.Token);
            
            Debug.Log("Start Moving Around");
            
            enemyBrain.StartMovingAround();
             
            Travel();
        });
    }


    private void FollowPlayer()
    {
        UniTask.Void(async  () =>
        {
            try
            { 
                await UniTask.Delay(TimeSpan.FromSeconds(0.1f), cancellationToken: _cts.Token);
                _navMeshAgent.destination = Vector3.Distance(transform.position, GameManager.playerRef.position) > stoppingDistance 
                    ? GameManager.playerRef.position : transform.position;
                FollowPlayer();
            }
            catch (Exception e)
            {
                Debug.Log("Thread Miss reference",this);
                Debug.Log(e);
                _cts.Cancel();
            }
            
        });
    }





}
