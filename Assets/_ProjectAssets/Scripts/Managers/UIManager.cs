using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    #region Singleton
    
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
    public Gradient lifeBarColor;
    public bool isPaused = false;

    [Header("Amo UI")] 
    public TextMeshProUGUI currentAmoText;
    public TextMeshProUGUI rezAmoText;

    [Header("Weapon UI")] 
    
    public Image armIcon,grenade;
    public Sprite pistol, AK;
    
    private void OnDisable()
    {
        Gunn.onShoot -= SetCurrentAmoUI;
        Gunn.onPickUpNewWeapon -= SetAmoUI;
        PlayerHealth.onPlayerGetDamage -= DecreaseHealthBarValue;
    }

    private void Start()
    {
        Gunn.onShoot += SetCurrentAmoUI;
        Gunn.onPickUpNewWeapon += SetAmoUI;
        PlayerHealth.onPlayerGetDamage += DecreaseHealthBarValue;
        UserInputController._pause.started += Pause;
        
    }

    public void SetImage(int type)
    {
        switch (type)
        {
            case 1:
                armIcon.sprite = pistol;
                break;
            case 2:
                armIcon.sprite = AK;
                break;
            case 3:
                armIcon.sprite = null;
                break;
        }
    }
    
   

    public void SetHealthBarMaxLife(float value)
    {
        playerHealthBar.maxValue = value;
        playerHealthBar.value = value;
        var current = playerHealthBar.colors;
        current.normalColor = lifeBarColor.Evaluate((float)(playerHealthBar.value/10));
        playerHealthBar.colors = current;
    }

    public void SetAmoUI(int currentAmo, int maxAmo)
    {
        currentAmoText.text = currentAmo.ToString();
        rezAmoText.text = maxAmo.ToString();
    }

    private void SetCurrentAmoUI()
    {
        int amo = int.Parse(currentAmoText.text);
        amo--;
        currentAmoText.text = amo.ToString();
    }
    
    public void DecreaseHealthBarValue(int value)
    {
        playerHealthBar.value -= value;
        var current = playerHealthBar.colors;
        current.normalColor = lifeBarColor.Evaluate((float)(playerHealthBar.value/10));
        playerHealthBar.colors = current;
        
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

    public void Die()
    {
        canvases[2].SetActive(true);
    }

    public void RestartGame()
    {
        ScenesManager.instance.ReloadCurrentScene();
        
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void HasGrenade()
    {
        Color c = grenade.color;
        c.a = 1;
        grenade.color=c;
    }
    
    public void NoGrenade()
    {
        Color c = grenade.color;
        c.a = 0.2f;
        grenade.color=c;
    }
}
