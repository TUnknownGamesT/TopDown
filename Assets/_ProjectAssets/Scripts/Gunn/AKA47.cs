using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AKA47 : Gunn
{

    public MeshRenderer[] meshRenderers;
    
    
    protected override bool CanShoot() => !reloading && TimeSinceLasrShot > 1f / (fireRate / 60f);

    public override void HighLight()
    {
        foreach (var mesh in meshRenderers)
        {
            mesh.material = Constants.instance.highLightInteractable;
        }
    }
    
    public override void UnHighLight()
    {
        foreach (var mesh in meshRenderers)
        {
            mesh.material = Constants.instance.unhighlightInteractable;
        }
    }
    
}
