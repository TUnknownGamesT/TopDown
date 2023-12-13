using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DoorBehaviour : MonoBehaviour,IInteractable
{
    public CustomBoxCollider _boxCollider;
    
    private MeshRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    public void Interact()
    {
        if (_boxCollider.CheckIfAllEnemiesDead())
        {
            OpenDoor();
        }
    }

    public void HighLight()
    {
        _renderer.material = Constants.instance.gunnHighLight;
    }

    public void UnHighLight()
    {
        _renderer.material = Constants.instance.gunnUnHighLight;
    }

    [ContextMenu("Open Door")]
    public void OpenDoor()
    {
       LeanTween.moveLocalY(gameObject, 6.3f, 1f).setEaseOutBounce();
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        HighLight();
    }

    private void OnTriggerExit(Collider other)
    {
        UnHighLight();
    }
}
