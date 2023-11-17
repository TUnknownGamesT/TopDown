using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyablOobject : MonoBehaviour
{
   public void DesTroy()
   {
      foreach (Transform children in transform)
      {
         MeshCollider meshCollider= children.AddComponent<MeshCollider>();
         meshCollider.convex = true;
         children.AddComponent<Rigidbody>();
      }

      UniTask.Void(async () =>
      {
         await UniTask.Delay(TimeSpan.FromSeconds(0.3f));
         gameObject.GetComponent<BoxCollider>().enabled = false;
      });
   }

   private void OnCollisionEnter(Collision collision)
   {
      if(collision.gameObject.CompareTag("Bullet"))
         DesTroy();
   }
}
