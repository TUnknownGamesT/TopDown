using System;
using UnityEngine;
using UnityEngine.UI;

public class AmbientMusicController : MonoBehaviour
{

    public AudioSource audioSource;

    private void OnEnable()
    {
        OptionsMenu.onAmbientVolumeValueChanged += SetVolume;
    }

    private void OnDisable()
    {
        OptionsMenu.onAmbientVolumeValueChanged -= SetVolume;
    }

    private void Awake()
    {
        audioSource.volume = OptionMenuBehaviour.instance.OptionsMenu.AmbientMusicVolume;
    }

    private void SetVolume(float value)
    {
        audioSource.volume = value;
    }


    
}
