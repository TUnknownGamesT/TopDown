using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableChecker : MonoBehaviour
{
    
    public IInteractable interactable;
    

    private void OnDisable()
    {
        UserInputController._spaceAction.started -= InteractWithLatsObject;
    }

    private void InteractWithLatsObject(InputAction.CallbackContext obj)
    {
        if (interactable != null)
        {
            interactable.Interact();
            interactable = null;
        }
    }


    private void Start()
    {
        UserInputController._spaceAction.started += InteractWithLatsObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IInteractable>()!=null)
        {
            Debug.Log("Interactable found");
            interactable = other.gameObject.GetComponent<IInteractable>();
        }
    }
}
