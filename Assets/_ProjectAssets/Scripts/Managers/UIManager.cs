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
    public Image lifeBar;
    public Gradient lifeBarColor;
    public bool isPaused = false;
    public OptionMenuBehaviour optionsMenuBehaviour;

    [Header("Amo UI")] 
    public TextMeshProUGUI currentAmoText;
    public TextMeshProUGUI rezAmoText;

    [Header("Weapon UI")] 
    
    public Image armIcon,grenade;

    public Sprite pistol, ak, shotgun;
    
    private void OnDisable()
    {
        Gunn.onShoot -= SetCurrentAmoUI;
        Gunn.onPickUpNewWeapon -= SetAmoUI;
        PlayerHealth.onPlayerGetDamage -= DecreaseHealthBarValue;
        UserInputController._pause.started -= Pause;
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
                armIcon.sprite = ak;
                break;
            case 3:
                armIcon.sprite = shotgun;
                break;
        }
    }
    
   

    public void SetHealthBarMaxLife(float value)
    {
        playerHealthBar.maxValue = value;
        playerHealthBar.value = value;
        lifeBar.color = lifeBarColor.Evaluate((playerHealthBar.normalizedValue));
    }

    private void SetAmoUI(int currentAmo, int maxAmo)
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

    private void DecreaseHealthBarValue(int value)
    {
        playerHealthBar.value -= value;
        lifeBar.color = lifeBarColor.Evaluate((playerHealthBar.normalizedValue));
    }

    public void RestartGame()
    {
        canvases[0].SetActive(false);
        canvases[1].SetActive(false);
        ScenesManager.instance.ReloadCurrentScene();
    }
    
    public void UnPause()
    {
        Pause(new InputAction.CallbackContext());
    }

    public void Pause(InputAction.CallbackContext obj)
    {
        Cursor.visible = !Cursor.visible;
        isPaused = !isPaused;
        canvases[0].SetActive(!isPaused);
        canvases[1].SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void EnableOptionsMenu()
    {
        Cursor.visible = true;
        optionsMenuBehaviour.EnableMainMenu();
        canvases[1].GetComponent<Canvas>().enabled = false;
        canvases[1].gameObject.GetComponent<GraphicRaycaster>().enabled = false;
    }

    public void CloseOptionMenu()
    {
        Cursor.visible = true;
        optionsMenuBehaviour.DisableMainMenu();
        canvases[1].GetComponent<Canvas>().enabled = true;
        canvases[1].gameObject.GetComponent<GraphicRaycaster>().enabled = true;
    }
    

    public void Die()
    {
        Cursor.visible = true;
        canvases[2].SetActive(true);
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
