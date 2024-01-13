using System;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody),typeof(SoundComponent))]
public class DoorBehaviour : MonoBehaviour,IInteractable
{

    public Vector3 cameraOffset;
    
    private AudioClip _openDoorSound;
    private MeshRenderer _renderer;
    private SoundComponent _soundComponent;

    private void Awake()
    {
        _soundComponent = GetComponent<SoundComponent>();
        _renderer = GetComponent<MeshRenderer>();
    }


    private void Start()
    {
        _openDoorSound = Constants.instance.openDoorSound;
    }

    public void QuickPressInteract()
    {
        _soundComponent.PlaySound(_openDoorSound);
        OpenDoor();
    }
    
    public void HoldInteract()
    {
        PlayerBrain.instance.DisableMove();
        CameraController.MoveCameraSmooth(cameraOffset);
        GlobalVolumeBehaviour.AddKeyHallEffect();
    }
    
    public void CancelHoldInteract()
    {
        PlayerBrain.instance.EnableMove();
        CameraController.MoveCameraSmooth(Vector3.zero);
        GlobalVolumeBehaviour.RemoveKeyHallEffect();
    }
    

    public void HighLight()
    {
        _renderer.material = Constants.instance.highLightInteractable;
    }

    public void UnHighLight()
    {
        _renderer.material = Constants.instance.unhighlightInteractable;
    }

    [ContextMenu("Open Door")]
    public void OpenDoor()
    {
        LeanTween.rotateY(gameObject, 90, 1f).setEaseInQuad();
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        HighLight();
    }

    private void OnTriggerExit(Collider other)
    {
        UnHighLight();
    }
    
    
    void OnGUI()
    {
        float sphereRadius = 100f;
        GUI.color = Color.red;
        // Convert world position to screen position
        Vector3 screenPos = Camera.main.WorldToScreenPoint(cameraOffset);

        // Calculate GUI position
        Vector2 guiPos = new Vector2(screenPos.x, Screen.height - screenPos.y);

        // Draw a sphere at the specified position
        GUI.DrawTexture(new Rect(guiPos.x - sphereRadius, guiPos.y - sphereRadius, 2 * sphereRadius, 2 * sphereRadius), EditorGUIUtility.whiteTexture);
    }
}
