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
    public List<Transform> travelPoints;
    public float stoppingDistance;
    public float pauseBetweenMovement;
    public EnemyBrain enemyBrain;
    
    private NavMeshAgent _navMeshAgent;
    private CancellationTokenSource _cts;
    private int _travelPointIndex;

    private void Awake()
    {
        enemyBrain = GetComponent<EnemyBrain>();
        _cts = new CancellationTokenSource();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }
    

    private void Start()
    {
        if (travelPoints.Count > 0)
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
        travelPoints.Add(GameManager.playerRef);
        Travel();
    }

    public void PlayerInView()
    {
        _cts.Cancel();
        _cts = new CancellationTokenSource();
        if(travelPoints.Contains(GameManager.playerRef))
            travelPoints.Remove(GameManager.playerRef);
        FollowPlayer();
    }
    
    public void Notice()
    {
        _cts.Cancel();
        _cts = new CancellationTokenSource();
        if(travelPoints.Contains(GameManager.playerRef)==false)
            travelPoints.Add(GameManager.playerRef);
        Travel();
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
            try
            {
                if(travelPoints.Count == 0)
                    return;
                _navMeshAgent.destination = travelPoints[_travelPointIndex].position;
                _travelPointIndex++;
                if(_travelPointIndex >= travelPoints.Count-1)
                    _travelPointIndex = 0;

                Debug.Log("START MOVEMENT");
            
                await UniTask.WaitUntil(()=>Vector3.Distance(transform.position,_navMeshAgent.destination) <= _navMeshAgent.stoppingDistance, cancellationToken: _cts.Token);
            
                Debug.Log("Finish Movement");
            
                enemyBrain.FinishMoving();

                Debug.Log("Start Looking Around");
            
                await UniTask.Delay(TimeSpan.FromSeconds(pauseBetweenMovement), cancellationToken: _cts.Token);
            
            
                Debug.Log("Start Moving Around");
            
                enemyBrain.StartMovingAround();
             
                Travel();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Debug.Log("Miss Reference");
            }
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
