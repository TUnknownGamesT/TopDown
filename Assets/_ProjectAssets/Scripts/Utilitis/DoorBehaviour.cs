using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody),typeof(SoundComponent))]
public class DoorBehaviour : MonoBehaviour,IInteractable
{

    public Vector3 cameraOffset;
    public GameObject interactableCanvas;
    
    private AudioClip _openDoorSound;
    private MeshRenderer _renderer;
    private SoundComponent _soundComponent;
    private Rigidbody _rigidbody;
    private bool _wasOpened = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
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
        if (interactableCanvas != null)
        {
            interactableCanvas.SetActive(true);
        }

    }

    public void UnHighLight()
    {
        _renderer.material = Constants.instance.unhighlightInteractable;
        if (interactableCanvas!=null)
        {
            interactableCanvas.SetActive(false);
        }
        
    }

    [ContextMenu("Open Door")]
    public void OpenDoor()
    {
        if (!_wasOpened)
        {
            LeanTween.rotateLocal(gameObject, new Vector3(0f, 90f, 0f), 1f).setEaseInQuad();
            _wasOpened = true;
        }
        
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        HighLight();
    }

    private void OnTriggerExit(Collider other)
    {
        UnHighLight();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            _rigidbody.isKinematic = false;
        }
            
    }
}
