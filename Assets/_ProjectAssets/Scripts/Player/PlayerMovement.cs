using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    
    private CharacterController _characterController;
    
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }
    
    private void Update()
    {
        Move();
    }
    
    public void Die()
    {
        speed = 0;
    }
    
    private void Move()
    {
        Vector2 direction = UserInputController._movementAction.ReadValue<Vector2>();
        _characterController.Move(new Vector3(direction.x, 0, direction.y)*Time.deltaTime *speed);
    }
}
