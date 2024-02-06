using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableChecker : MonoBehaviour
{
    
    public static Action<GameObject> onGrenadePickUp;
    
    public IInteractable interactable;

    private bool isHolding;
    public CharacterController _characterController;
    
    private void OnDisable()
    {
        UserInputController._EAction.started -= InteractWithLatsObject;
        UserInputController._EAction.performed-= HoldInteract;
        UserInputController._EAction.canceled -= StopInteraction;
    }

    private void Start()
    {
        UserInputController._EAction.started += InteractWithLatsObject;
        UserInputController._EAction.performed += HoldInteract;
        UserInputController._EAction.canceled += StopInteraction;
    }
    
    private void InteractWithLatsObject(InputAction.CallbackContext obj)
    {
        UniTask.Void(async () =>
        {
           await UniTask.Delay(TimeSpan.FromSeconds(0.2));
            if (interactable != null&& !isHolding)
            {
                interactable.QuickPressInteract();
                interactable = null;
            }
        });
    }

    private void HoldInteract(InputAction.CallbackContext callbackContext)
    {
        isHolding = true;
        UniTask.Void(async () =>
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.1));
            if (interactable != null&& isHolding)
            {
                Debug.Log("Hold Interact");
                interactable.HoldInteract();
                
            }
        });
    }

    private void StopInteraction(InputAction.CallbackContext callbackContext)
    {
        if (interactable != null)
        {
            interactable.CancelHoldInteract();
            interactable = null;
        }
        isHolding= false;
    }
    


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<IInteractable>()!=null)
        {
            Debug.Log("Interactable found");
            interactable = other.gameObject.GetComponent<IInteractable>();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grenade"))
        {
            onGrenadePickUp?.Invoke(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<IInteractable>() != null)
        {
            interactable = null;
        }
    }
}
