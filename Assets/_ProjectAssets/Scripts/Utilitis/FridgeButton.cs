using System;
using UnityEngine;

public class FridgeButton : MonoBehaviour, IInteractable
{

    public CustomBoxCollider customBoxCollider;
    public ParticleSystem snow;
    
    private MeshRenderer _renderer;
    

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    public void QuickPressInteract()
    {
        snow.Play();
        customBoxCollider.KillAllEnemies();
    }

    public void HighLight()
    {
        _renderer.material = Constants.instance.highLightInteractable;
    }

    public void UnHighLight()
    {
        _renderer.material = Constants.instance.unhighlightInteractable;
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
