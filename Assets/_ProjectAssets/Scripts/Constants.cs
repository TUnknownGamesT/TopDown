using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Constants : MonoBehaviour
{
   #region Singleton

   public static Constants instance;

   private void Awake()
   {
      instance = FindObjectOfType<Constants>();
      
      if (instance == null)
      {
         instance = this;
      }
   }

   #endregion
   
   public enum EnemyType
   {
      Male=0,
      Pistol=1,
      AKA47=2,
      Sniper=3,
      
   }

   public static int PistolEnemyHealth = 2;
   public static int Ak47EnemyHealth = 1;
   public static int MaleEnemyHealth = 1;

   public static float meleEnemyAttackDistace = 1.8f;
   public static float rangeStoppingDistance = 6;

   public AudioClip openDoorSound;

   [FormerlySerializedAs("gunnHighLight")] public Material highLightInteractable;
   [FormerlySerializedAs("gunnUnHighLight")] public Material unhighlightInteractable;

}
