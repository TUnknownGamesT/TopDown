using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotation : MonoBehaviour
{
    public float damping;
    
    private bool _staringAtPlayer;
    
    public void PlayerInView()
    {
        _staringAtPlayer = true;
    }
    
    public void PlayerOutOfView()
    {
        _staringAtPlayer = false;
    }
   

    // Update is called once per frame
    void Update()
    {
        if(_staringAtPlayer)
            RotateTowardThePlayer();
    }

    public void PlayerDeath()
    {
        _staringAtPlayer = false;
    }
    
    
    
    private void RotateTowardThePlayer()
    {
        var lookPos = GameManager.playerRef.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation,Time.deltaTime * damping);
    }
}
