using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Transform playerRef;


    private void Awake()
    {
        playerRef =  GameObject.FindGameObjectWithTag("Player").transform;
    }
}
