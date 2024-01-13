using System;
using UnityEngine;

public class FridgeButton : MonoBehaviour, IInteractable
{

    public CustomBoxCollider customBoxCollider;
    
    private MeshRenderer _renderer;
    

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    public void QuickPressInteract()
    {
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
}
