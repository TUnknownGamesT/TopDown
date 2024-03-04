using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class FootStepsSound : MonoBehaviour
{
    public AudioClip footstepSound; // Array to hold footstep sound clips
    public float minTimeBetweenFootsteps = 0.3f; // Minimum time between footstep sounds
    public float maxTimeBetweenFootsteps = 0.6f; // Maximum time between footstep sounds

    private AudioSource audioSource; // Reference to the Audio Source component
    private bool isWalking = false; // Flag to track if the player is walking
    private float timeSinceLastFootstep; // Time since the last footstep sound

    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        // Get the Audio Source component
    }

    private void Start()
    {
        audioSource.volume =
            OptionMenuBehaviour.instance.OptionsMenu.SoundEffectVolume;
    }

    private void OnEnable()
    {
        OptionsMenu.onSoundEffectVolumeValueChanged += SetVolume;
        
    }
    
    private void OnDisable()
    {
        OptionsMenu.onSoundEffectVolumeValueChanged -=SetVolume;
    }

    private void Update()
    {
        // Check if the player is walking
        if (isWalking)
        {
            // Check if enough time has passed to play the next footstep sound
            if (Time.time - timeSinceLastFootstep >= Random.Range(minTimeBetweenFootsteps, maxTimeBetweenFootsteps))
            {
                // Play a random footstep sound from the array
                audioSource.PlayOneShot(footstepSound);

                timeSinceLastFootstep = Time.time; // Update the time since the last footstep sound
            }
        }
    }

    // Call this method when the player starts walking
    public void StartWalking()
    {
        isWalking = true;
    }

    // Call this method when the player stops walking
    public void StopWalking()
    {
        isWalking = false;
    }

    private void SetVolume(float value)
    {
        audioSource.volume = value;
    }
}
