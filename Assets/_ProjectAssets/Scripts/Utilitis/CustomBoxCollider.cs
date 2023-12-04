using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomBoxCollider : MonoBehaviour
{

    public LayerMask targetMask;


    private void Awake()
    {
        //targetMask = LayerMask.NameToLayer("Enemy");
    }

    public bool CheckIfAllEnemiesDead()
    {
        Collider[] rangeChecks =
            Physics.OverlapBox(transform.position, transform.localScale/2,Quaternion.identity , targetMask, QueryTriggerInteraction.Collide);
        Debug.Log(rangeChecks.Length);
        return rangeChecks.Length == 0; 
    }
    
    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent red cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position, transform.localScale.ReverseXZ());
    }
}


public static class Vector3Extension
{
    public static Vector3 ReverseXZ(this Vector3 vector3)
    {
        return new Vector3(vector3.z, vector3.y, vector3.x);
    }
}
