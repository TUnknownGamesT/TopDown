using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    
    
    public Slider slider;


    private void OnEnable()
    {
        PlayerHealth.onPlayerGetDamage += DecreaseHealthBarValue;
    }
    
    private void OnDisable()
    {
        PlayerHealth.onPlayerGetDamage -= DecreaseHealthBarValue;
    }

    public void SetHealthBarMaxLife(float value)
    {
        slider.maxValue = value;
        slider.value = value;
    }

    public void DecreaseHealthBarValue(int value)
    {
        slider.value -= value;
    }
}
