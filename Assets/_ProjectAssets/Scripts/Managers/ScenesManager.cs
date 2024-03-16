using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
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
    public GameObject video;
    
    
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

    public void LoadNextScene()
    {
        if (!isLoading)
        {
            isLoading = !isLoading;
            TransitionManager.Instance()
                .Transition(SceneManager.GetActiveScene().buildIndex +1
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
    
    public void LoadScene(int index)
    {
        if (!isLoading)
        {
            isLoading = !isLoading;
            TransitionManager.Instance()
                .Transition(index,TransitionSettings[Random.Range(0,TransitionSettings.Length-1)],0);
            onSceneNewSceneLoad?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UniTask.Void(async () =>
            {
                video.SetActive(true);
                await UniTask.Delay(TimeSpan.FromSeconds(7));
                LoadNextScene();
            });
            
        }
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
    
}
