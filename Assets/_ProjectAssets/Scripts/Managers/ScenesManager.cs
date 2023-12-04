using System;
using System.Collections;
using System.Collections.Generic;
using EasyTransition;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class ScenesManager : MonoBehaviour
{

    public static Action onSceneNewSceneLoad;
    
    #region Singleton
    
    public static ScenesManager instance;
    
    private void Awake()
    {
        instance = FindObjectOfType<ScenesManager>();
        if (instance == null)
        {
            instance = this;
        }
    }
    
    #endregion
    
    public TransitionSettings[] TransitionSettings;
    public int gameSceneIndex;
    
    
    [HideInInspector]
    public bool isLoading;
    

    public void ReloadCurrentScene()
    {
        if (!isLoading)
        {
            isLoading = !isLoading;
            TransitionManager.Instance()
                .Transition(SceneManager.GetActiveScene().buildIndex
                    ,TransitionSettings[Random.Range(0,TransitionSettings.Length-1)],0);
            onSceneNewSceneLoad?.Invoke();
        }
    }
    
    public  void LoadGameScene()
    {
        if (!isLoading)
        {
            isLoading = !isLoading;
            TransitionManager.Instance()
                .Transition(gameSceneIndex,TransitionSettings[Random.Range(0,TransitionSettings.Length-1)]
                    ,0);
        }
    }
}
