using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody),typeof(SoundComponent))]
public class DoorBehaviour : MonoBehaviour,IInteractable
{
    public CustomBoxCollider _boxCollider;
    public AudioClip openDoorSound;
    public AudioClip enemiesInTheRoomSound;


    private MeshRenderer _renderer;
    private SoundComponent _soundComponent;

    private void Awake()
    {
        _soundComponent = GetComponent<SoundComponent>();
        _renderer = GetComponent<MeshRenderer>();
    }

    public void Interact()
    {
        if (_boxCollider.CheckIfAllEnemiesDead())
        {
            _soundComponent.PlaySound(openDoorSound);
            OpenDoor();
        }
        else
        {
            _soundComponent.PlaySound(enemiesInTheRoomSound);
            EnemiesInTheRoom();
        }
    }
    
    
    private void EnemiesInTheRoom()
    {
        UniTask.Void(async () =>
        {
            Color color = _renderer.material.GetColor("_EmissionColor");
            _renderer.material.SetColor("_EmissionColor", Color.red);
            _renderer.material.color=Color.red;
            await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
            _renderer.material.color=color;
            _renderer.material.SetColor("_EmissionColor", color);
            await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
            _renderer.material.color=Color.red;
            _renderer.material.SetColor("_EmissionColor", Color.red);
            await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
            _renderer.material.color=color;
            _renderer.material.SetColor("_EmissionColor", color);
        });
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
