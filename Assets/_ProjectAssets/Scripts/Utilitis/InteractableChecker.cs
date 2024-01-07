using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableChecker : MonoBehaviour
{
    
    public IInteractable interactable;

    private bool isHolding;
    public CharacterController _characterController;
    
    private void OnDisable()
    {
        UserInputController._spaceAction.started -= InteractWithLatsObject;
        UserInputController._spaceAction.performed-= HoldInteract;
        UserInputController._spaceAction.canceled -= StopInteraction;
    }

    private void Start()
    {
        UserInputController._spaceAction.started += InteractWithLatsObject;
        UserInputController._spaceAction.performed += HoldInteract;
        UserInputController._spaceAction.canceled += StopInteraction;
    }
    
    private void InteractWithLatsObject(InputAction.CallbackContext obj)
    {
        UniTask.Void(async () =>
        {
           await UniTask.Delay(TimeSpan.FromSeconds(0.1));
            if (interactable != null&& !isHolding)
            {
                Debug.Log("Quick Interact");
                interactable.QuickPressInteract();
            }
        });
    }

    private void HoldInteract(InputAction.CallbackContext callbackContext)
    {
        isHolding = true;
        if (interactable != null)
        {
            interactable.HoldInteract();
        }
    }

    private void StopInteraction(InputAction.CallbackContext callbackContext)
    {
        if (interactable != null)
        {
            interactable.CancelHoldInteract();
            isHolding= false;
        }
    }
    


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IInteractable>()!=null)
        {
            Debug.Log("Interactable found");
            interactable = other.gameObject.GetComponent<IInteractable>();
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
