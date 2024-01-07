using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;

public class PlayerBrain : MonoBehaviour
{
    #region Singleton
    
    public static PlayerBrain instance;
    
    private void Awake()
    {
        instance = FindObjectOfType<PlayerBrain>();
        
        if (instance == null)
        {
            instance = this;
        }
        
        playerMovement = GetComponent<PlayerMovement>();
        playerHealth = GetComponent<PlayerHealth>();
        playerRotation = GetComponent<PlayerRotation>();
        playerArmHandler = GetComponent<PlayerArmHandler>();
    }
    

    #endregion

    private PlayerMovement playerMovement;
    private  PlayerHealth playerHealth;
    private PlayerRotation playerRotation;
    private PlayerArmHandler playerArmHandler;
    

    public void DisableMove()
    {
        playerMovement.enabled = false;
        playerRotation.enabled = false;
    }

    public void EnableMove()
    {
        playerMovement.enabled = true;
        playerRotation.enabled = true;
    }
}
