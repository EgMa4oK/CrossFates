using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class InteractionInstigator : MonoBehaviour
{
    private List<Interactable> nearbyInteractables = new List<Interactable>();
    public event Action InteractableAdded;
    public event Action InteractableRemoved;
    public event Action OnInteraction;
    private PlayerInput Input;

    private void Awake()
    {
        Input = InputManager.Input;
        
    }

    private void OnEnable()
    {
        Input.Player.Interact.performed += Crutch;
    }

    private void OnDisable()
    {
        Input.Player.Interact.performed -= Crutch;
    }

    public bool HasNearbyInteractables()
    {
        return nearbyInteractables.Count != 0;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null && interactable.CanActivate())
        {       
            nearbyInteractables.Add(interactable);
            InteractableAdded?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null)
        {        
            nearbyInteractables.Remove(interactable);
            InteractableRemoved?.Invoke();
        }
    }

    private void Interact()
    {
        if (HasNearbyInteractables() && Input.Player.Interact.WasPressedThisFrame())
        {
            nearbyInteractables[0].Activate();
            OnInteraction?.Invoke();
        }
    }

    private void Crutch(InputAction.CallbackContext a)
    {
        Interact();
    }

}