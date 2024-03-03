using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    #region Singleton

    public static GameManager instance;
    
    private void Awake()
    {
        instance = FindObjectOfType<GameManager>();

        if (instance == null)
        {
            instance = this;
        }
        
        Cursor.visible = false; 
        playerRef =  GameObject.FindGameObjectWithTag("Player").transform;
        crossHair =  GameObject.FindGameObjectWithTag("CrossHair").transform;
    }

    #endregion
    
    public static Transform playerRef;
    public static Transform crossHair;
    


    
}
