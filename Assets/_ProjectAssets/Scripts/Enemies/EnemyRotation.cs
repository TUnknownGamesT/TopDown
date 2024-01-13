using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class EnemyRotation : MonoBehaviour
{
    public float damping;
    public Vector2 rotationAngles;
    public float pauseBetweenRotation;

    private bool _staringAtPlayer;
    private bool _lookAround = true;
    private float _angle;
    private bool _rotationDirection;
    private Quaternion _rotation;
    private float _rotationVelocity;
    private CancellationTokenSource _cts;
    private Vector2 _dynamicRotation;

    public void PlayerInView()
    {
        _cts.Cancel();
        _staringAtPlayer = true;
    }

    public void PlayerOutOfView()
    {
        _cts = new CancellationTokenSource();
        _staringAtPlayer = false;
    }

    private void Start()
    {
        _cts = new CancellationTokenSource();

        _dynamicRotation.x = transform.rotation.eulerAngles.y + rotationAngles.x;
        _dynamicRotation.y = transform.rotation.eulerAngles.y - rotationAngles.y;
        
        _angle = _dynamicRotation.x;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_staringAtPlayer)
        {
            RotateTowardThePlayer();
        }
        
    }

    public void StopLookingAround()
    {
        _cts.Cancel();
        _cts = new CancellationTokenSource();
    }
    
    public void StartLookingAround()
    {
        _cts = new CancellationTokenSource();
        
        _dynamicRotation.x = transform.rotation.eulerAngles.y + rotationAngles.x;
        _dynamicRotation.y = transform.rotation.eulerAngles.y - rotationAngles.y;
        
        RotateAround();
    }

    public void PlayerDeath()
    {
        _cts.Cancel();
        _staringAtPlayer = false;
    }


    private void RotateTowardThePlayer()
    {
        var lookPos = GameManager.playerRef.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
    }

    public void RotateAround()
    {
        UniTask.Void(async () =>
        {
            float angle = Mathf.SmoothDampAngle( transform.eulerAngles.y, _angle, ref _rotationVelocity, 1f);
            transform.rotation = Quaternion.Euler( transform.eulerAngles.x, angle,  transform.eulerAngles.z);
            if (Math.Abs(angle - _angle) < 2)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(pauseBetweenRotation), cancellationToken: _cts.Token);
                _angle = _rotationDirection ? _dynamicRotation.x : _dynamicRotation.y;
                _rotationDirection= !_rotationDirection;
            }
            
            await UniTask.Delay(TimeSpan.FromSeconds(0), cancellationToken: _cts.Token);
            RotateAround();
        });
    }
}