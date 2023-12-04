using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DoorBehaviour : MonoBehaviour,IInteractable
{
    public CustomBoxCollider _boxCollider;
    
    public void Interact()
    {
        if (_boxCollider.CheckIfAllEnemiesDead())
        {
            OpenDoor();
        }
    }

    public void HighLight()
    {
       
    }

    public void UnHighLight()
    {
        
    }

    [ContextMenu("Open Door")]
    public void OpenDoor()
    {
       LeanTween.moveLocalY(gameObject, 6.3f, 1f).setEaseOutBounce();
    }
}
