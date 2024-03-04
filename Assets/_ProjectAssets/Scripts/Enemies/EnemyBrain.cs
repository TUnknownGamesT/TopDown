using System;
using Cysharp.Threading.Tasks;
using UnityEngine;


public class EnemyBrain : MonoBehaviour
{
    
    public Constants.EnemyType enemyType;
    public AudioClip deathSound;
    public bool _alreadyInView;
    
    private EnemyMovement _enemyMovement;
    private EnemyHealth _enemyHealth;
    private FieldOfView _fieldOfView;
    private EnemyRotation _enemyRotation;
    private EnemyArms _enemyArms;
    private SoundComponent _soundComponent;
    private EnemyAnimations _enemyAnimations;
    
    
    private bool noticed;
    private CapsuleCollider _capsuleCollider;
    private bool dead;
    

    private void Awake()
    {
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _soundComponent = GetComponent<SoundComponent>();
        _enemyHealth = GetComponent<EnemyHealth>();
        _fieldOfView = GetComponent<FieldOfView>();
        _enemyRotation = GetComponent<EnemyRotation>();
        _enemyArms = GetComponent<EnemyArms>();
        _enemyMovement = GetComponent<EnemyMovement>();
        _enemyAnimations = GetComponent<EnemyAnimations>();
    }

    private void Start()
    {
        InitStats(EnemyInitiator.instance.GetEnemyStats(enemyType));
    }

    private void InitStats(EnemyType enemyType)
    {
        _enemyArms.InitStats(enemyType);
        _enemyAnimations.SetWeapon(this.enemyType);
        if(_enemyMovement != null)
            _enemyMovement.stoppingDistance = enemyType.stoppingDistance;    
    }

    public void PlayerInView()
    {
        _alreadyInView = true;
        _enemyRotation.PlayerInView();
        _enemyArms.Shoot();
        _enemyMovement?.PlayerInView();
        Notice();
    }

    public void Notice()
    {
        if (!noticed && !_alreadyInView)
        {
            _alreadyInView = true;
            noticed= true;
            EnemyInitiator.instance.InstantiateAlert(transform.position);
            _enemyRotation.StopLookingAround();
            _enemyMovement.Notice();
        }
        else
        {
            UniTask.Void(async () =>
            {
                await UniTask.Delay(TimeSpan.FromSeconds(3));
                noticed= false;
            });
        }
    }


    public void PlayerOutOfView()
    {
        _alreadyInView = false;
        Debug.Log("Player out of view Enemy brain");
        _enemyRotation.PlayerOutOfView();
        _enemyMovement?.PlayerOutOfView();
    }
    
    
    public void Death()
    {
        if (dead)
            return;
        Notice();
        dead = true;
        _capsuleCollider.enabled = false;
        _soundComponent.PlaySound(deathSound);
        _fieldOfView.EnemyDeath();
       _enemyMovement?.EnemyDeath();
       _enemyRotation.PlayerDeath();
       _enemyArms.DropArm();
    }
    
    public void FinishMoving()
    {
        _enemyRotation.StartLookingAround();
    }

    public void StartMovingAround()
    {
        _enemyRotation.StopLookingAround();
    }

    private void Arm()
    {
       
    }
}
