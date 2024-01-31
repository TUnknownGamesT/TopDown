using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Transform playerRef;


    private void Awake()
    {
        Cursor.visible = false; 
        playerRef =  GameObject.FindGameObjectWithTag("Player").transform;
    }
}
