using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    
    [Header("Shaking")]
    public static  CinemachineVirtualCamera virtualCamera;
    public static  float shakeDuration = 0.2f;
    public static float shakeAmplitude = 1.2f;
    
    private static float shakeTimeRemain;


    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        if(shakeTimeRemain >0)
        {
            shakeTimeRemain -= Time.deltaTime;
            if (shakeTimeRemain <= 0)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                    virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
            }
        }
    }
    
    public static void ShakeCamera()
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = shakeAmplitude;
        shakeTimeRemain = shakeDuration;

    }

    public static void ExplosionCameraShake()
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 8f;
        shakeTimeRemain = 1f;
    }
}
