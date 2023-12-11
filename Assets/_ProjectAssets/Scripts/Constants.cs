using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
      Pistol=1,
      AKA47=2,
      Male=0
      
   }

   public static int PistolEnemyHealth = 2;
   public static int Ak47EnemyHealth = 1;
   public static int MaleEnemyHealth = 1;

   public static float meleEnemyAttackDistace = 1.8f;
   public static float rangeStoppingDistance = 6;
   
   
   
   public Material gunnHighLight;
   public Material gunnUnHighLight;

}
