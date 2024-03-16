using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class LVl2Manager : MonoBehaviour
{
   public PlayerMovement playerMovement;
   public GameObject robertoCamera;
   public ScenesManager sceneManager;
   public CustomBoxCollider customBoxCollider;
   public GameObject video;

   private void OnEnable()
   {
      EnemyBrain.onEnemyDie += NextLvl;
   }

   private void OnDisable()
   {
      EnemyBrain.onEnemyDie -= NextLvl;
   }


   private void NextLvl()
   {
      if (customBoxCollider.GetEnemyNumbers() == 0)
      {
         UniTask.Void(async () =>
         {
            video.SetActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(6));
            sceneManager.LoadNextScene();
         });
      }
   }

   private void EnableBossFightInstructions()
   {
      UniTask.Void(async () =>
      {
         playerMovement.enabled = false;
         robertoCamera.SetActive(true);
         await UniTask.Delay(TimeSpan.FromSeconds(2));

         LeanTween.moveZ(robertoCamera, -18, 3f).setEaseInQuad();
         
         await UniTask.Delay(TimeSpan.FromSeconds(4));
         
         playerMovement.enabled = true;
         robertoCamera.SetActive(false);
      });
      
   }
   

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         EnableBossFightInstructions();
         GetComponent<BoxCollider>().enabled = false;
      }
   }
}
