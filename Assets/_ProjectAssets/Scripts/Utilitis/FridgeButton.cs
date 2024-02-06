using System;
using System.Collections.Generic;
using UnityEngine;

public class FridgeButton : MonoBehaviour, IInteractable
{
    public List<GameObject> interactions;
    public CustomBoxCollider customBoxCollider;
    public ParticleSystem snow;
    public GameObject interactableCanvas;
    private MeshRenderer _renderer;
    

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    public void QuickPressInteract()
    {
        snow.Play();
        foreach (var obj in interactions)
        {
            obj.GetComponent<ISpecialInteraction>()?.Interact();
        }
        //customBoxCollider.KillAllEnemies();
    }

    public void HighLight()
    {
        _renderer.material = Constants.instance.highLightInteractable;
        if (interactableCanvas!=null)
        {
            interactableCanvas.SetActive(true);
        }
    }

    public void UnHighLight()
    {
        _renderer.material = Constants.instance.unhighlightInteractable;
        if (interactableCanvas!=null)
        {
            interactableCanvas.SetActive(false);
        }
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
