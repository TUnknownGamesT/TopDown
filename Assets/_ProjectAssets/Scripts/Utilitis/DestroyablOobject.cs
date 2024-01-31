using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DestroyablOobject : MonoBehaviour
{
   private bool alreadyDestroyd;
   
   public void DesTroy(Vector3 direction)
   {
      foreach (Transform children in transform)
      {
         MeshCollider meshCollider= children.AddComponent<MeshCollider>();
         meshCollider.convex = true;
         Rigidbody _rb =  children.AddComponent<Rigidbody>();
         _rb.AddForce(direction.normalized * 100, ForceMode.Impulse);
      }

      foreach (Transform children in transform)
      {
         children.SetParent(null);
      }

      UniTask.Void(async () =>
      {
         await UniTask.Delay(TimeSpan.FromSeconds(0.3f));
         gameObject.GetComponent<BoxCollider>().enabled = false;
      });
   }
   
}
