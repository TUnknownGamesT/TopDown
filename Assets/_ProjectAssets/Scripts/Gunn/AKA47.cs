using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AKA47 : Gunn
{

    public MeshRenderer[] meshRenderers;
    
    public override void HighLight()
    {
        foreach (var mesh in meshRenderers)
        {
            mesh.material = Constants.instance.gunnHighLight;
        }
    }
    
    public override void UnHighLight()
    {
        foreach (var mesh in meshRenderers)
        {
            mesh.material = Constants.instance.gunnUnHighLight;
        }
    }
    
}
