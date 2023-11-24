using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    
    private EnemyMovement _enemyMovement;
    private EnemyHealth _enemyHealth;
    private FieldOfView _fieldOfView;
    private EnemyRotation _enemyRotation;
    private EnemyArms _enemyArms;
    

    private void Awake()
    {
        _enemyHealth = GetComponent<EnemyHealth>();
        _fieldOfView = GetComponent<FieldOfView>();
        _enemyRotation = GetComponent<EnemyRotation>();
        _enemyArms = GetComponent<EnemyArms>();
        _enemyMovement = GetComponent<EnemyMovement>();
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
        _fieldOfView.EnemyDeath();
       _enemyMovement?.EnemyDeath();
       _enemyArms.DropArm();
    }

    private void Arm()
    {
       
    }
}
