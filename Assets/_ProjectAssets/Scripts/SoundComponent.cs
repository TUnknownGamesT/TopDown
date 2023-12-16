using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundComponent : MonoBehaviour
{
    
    private AudioSource _audioSource;
    
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.minDistance = 15.80656f;
    }
    
    
    public void PlaySound(AudioClip sound)
    {
        _audioSource.PlayOneShot(sound);
    }
}
