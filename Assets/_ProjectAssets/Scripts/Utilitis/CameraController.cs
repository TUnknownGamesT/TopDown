using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    [Header("Shaking")]
    public static  CinemachineVirtualCamera virtualCamera;
    public static  float shakeDuration = 0.2f;
    public static float shakeAmplitude = 1.2f;

    private static float shakeTimeRemain;
    private static Transform _cameraTransform;
    private static CinemachineCameraOffset _cinemachineCameraOffset;

    private void Awake()
    {
        _cameraTransform = gameObject.transform;
        _cinemachineCameraOffset = GetComponent<CinemachineCameraOffset>();
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

    public static void MoveCameraSmooth(Vector3 newPosition)
    {
        Debug.Log("Moving");
        LeanTween.value(0, 1, 0.5f).setOnUpdate((float value) =>
        {
            _cinemachineCameraOffset.m_Offset = Vector3.Lerp(_cinemachineCameraOffset.m_Offset, newPosition, value);
        }).setEaseInQuad();
    }
}
