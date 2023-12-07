using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    #region Sinfleton
    
    public static UIManager instance;

    private void Awake()
    {
        instance = FindObjectOfType<UIManager>();
        if (instance == null)
        {
            instance = this;
        }
    }

    #endregion
    [SerializeField]
    private List<GameObject> canvases;
    public Slider playerHealthBar;
    public bool isPaused = false;


    private void Start()
    {
        PlayerHealth.onPlayerGetDamage += DecreaseHealthBarValue;
        UserInputController._pause.performed += Pause;
    }
    
    private void OnDisable()
    {
        PlayerHealth.onPlayerGetDamage -= DecreaseHealthBarValue;
    }

    public void SetHealthBarMaxLife(float value)
    {
        playerHealthBar.maxValue = value;
        playerHealthBar.value = value;
    }

    public void DecreaseHealthBarValue(int value)
    {
        playerHealthBar.value -= value;
    }

    public void GameLost()
    {
        canvases[0].SetActive(false);
        canvases[2].SetActive(true);
        Time.timeScale = 0.1f;
    }

    public void UnPause()
    {
        Pause(new InputAction.CallbackContext());
    }

    public void Pause(InputAction.CallbackContext obj)
    {
        isPaused = !isPaused;
        canvases[0].SetActive(!isPaused);
        canvases[1].SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
        
    }

    public void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        // Reload the current scene
        SceneManager.LoadScene(currentScene.name);
        
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
}
