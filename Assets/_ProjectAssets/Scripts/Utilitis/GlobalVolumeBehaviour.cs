using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GlobalVolumeBehaviour : MonoBehaviour
{
    private static Volume _volume;

    private static Vignette _vignette;

    private void Awake()
    {
        _volume = GetComponent<Volume>();
        _volume.profile.TryGet(out _vignette);
    }

    public static void AddKeyHallEffect()
    {
        _vignette.intensity.value = 0.6f;
    }
    
    public static void RemoveKeyHallEffect()
    {
        _vignette.intensity.value = 0.447f;
    }
}
