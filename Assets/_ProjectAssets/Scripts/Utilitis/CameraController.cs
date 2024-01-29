using System;
using Cinemachine;
using MoreMountains.TopDownEngine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    
    [Header("Shaking")]
    public static  CinemachineVirtualCamera virtualCamera;
    public static  float shakeDuration = 0.2f;
    public static float shakeAmplitude = 1.2f;
    public Transform forwardPosition;
    
    private static float shakeTimeRemain;
    private static Transform _cameraTransform;
    private static CinemachineCameraOffset _cinemachineCameraOffset;
    private  CinemachineVirtualCamera _cinemachineVirtualCamera;

    private void Awake()
    {
        _cameraTransform = gameObject.transform;
        _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        _cinemachineCameraOffset = GetComponent<CinemachineCameraOffset>();
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void OnDisable()
    {
        UserInputController._rightClick.started -= LookForward;
        UserInputController._rightClick.canceled -= LookAtPlayer;
    }

    private void Start()
    {
        UserInputController._rightClick.started += LookForward;
        UserInputController._rightClick.canceled += LookAtPlayer;
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

    private  void LookForward(InputAction.CallbackContext callbackContext)
    {
        _cinemachineVirtualCamera.LookAt = forwardPosition;
        _cinemachineVirtualCamera.Follow = forwardPosition;
    }
    
    private void LookAtPlayer(InputAction.CallbackContext callbackContext)
    {
        _cinemachineVirtualCamera.Follow = GameManager.playerRef;
        _cinemachineVirtualCamera.LookAt = GameManager.playerRef;
    }
}
