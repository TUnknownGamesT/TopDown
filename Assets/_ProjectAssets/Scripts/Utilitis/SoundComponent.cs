using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundComponent : MonoBehaviour
{
    
    private AudioSource _audioSource;
    
    private void Awake()
    {

        _audioSource = GetComponent<AudioSource>();
        _audioSource.minDistance = 15.80656f;
        _audioSource.volume = OptionMenuBehaviour.instance.OptionsMenu.AmbientMusicVolume;
    }

    public void OnEnable()
    {
        OptionsMenu.onSoundEffectVolumeValueChanged += SetSoundEffectsSound;
    }

    public void OnDisable()
    {
        OptionsMenu.onSoundEffectVolumeValueChanged += SetSoundEffectsSound;
    }

    public void PlaySound(AudioClip sound)
    {
        _audioSource.PlayOneShot(sound);
    }

    private void SetSoundEffectsSound(float value)
    {
        _audioSource.volume = value;
    }
}
