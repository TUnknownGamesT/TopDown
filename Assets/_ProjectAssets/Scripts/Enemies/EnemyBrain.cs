using UnityEngine;


public class EnemyBrain : MonoBehaviour
{
    
    public Constants.EnemyType enemyType;
    public AudioClip deathSound;
    
    private EnemyMovement _enemyMovement;
    private EnemyHealth _enemyHealth;
    private FieldOfView _fieldOfView;
    private EnemyRotation _enemyRotation;
    private EnemyArms _enemyArms;
    private SoundComponent _soundComponent;
    

    private void Awake()
    {
        _soundComponent = GetComponent<SoundComponent>();
        _enemyHealth = GetComponent<EnemyHealth>();
        _fieldOfView = GetComponent<FieldOfView>();
        _enemyRotation = GetComponent<EnemyRotation>();
        _enemyArms = GetComponent<EnemyArms>();
        _enemyMovement = GetComponent<EnemyMovement>();
        
    }

    private void Start()
    {
        InitStats(EnemyInitiator.instance.GetEnemyStats(enemyType));
    }

    private void InitStats(EnemyType enemyType)
    {
        _enemyArms.InitStats(enemyType);
        if(_enemyMovement != null)
            _enemyMovement.stoppingDistance = enemyType.stoppingDistance;    
    }

    public void PlayerInView()
    {
        _enemyRotation.PlayerInView();
        _enemyArms.Shoot();
        _enemyMovement?.PlayerInView();
    }


    public void PlayerOutOfView()
    {
        _enemyRotation.PlayerOutOfView();
        _enemyMovement?.PlayerOutOfView();
    }
    
    
    public void Death()
    {
        _soundComponent.PlaySound(deathSound);
        _fieldOfView.EnemyDeath();
       _enemyMovement?.EnemyDeath();
       _enemyRotation.PlayerDeath();
       _enemyArms.DropArm();
    }

    private void Arm()
    {
       
    }
}
