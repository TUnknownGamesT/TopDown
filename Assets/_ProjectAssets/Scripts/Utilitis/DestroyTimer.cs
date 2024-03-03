using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    
    public float timeBeforeDestroy;


    private AudioSource _audioSource;


    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = OptionMenuBehaviour.instance.OptionsMenu.SoundEffectVolume;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        Destroy(gameObject,timeBeforeDestroy);
    }
    
}
