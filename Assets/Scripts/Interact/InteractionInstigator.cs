using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

namespace CrossFates
{
    public class InteractionInstigator : MonoBehaviour
    {
        private List<Interactable> nearbyInteractables = new List<Interactable>();
        public event Action InteractableAdded;
        public event Action InteractableRemoved;
        public event Action OnInteraction;
        private Controls Input;

        private void Awake()
        {
            Input = new Controls();

        }

        private void OnEnable()
        {
            Input.Enable();
            Input.Character.Interact.performed += Interact;
        }

        private void OnDisable()
        {
            Input.Disable();
            Input.Character.Interact.performed -= Interact;
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

        private void Interact(InputAction.CallbackContext a)
        {
            print("Sddd");
            if (HasNearbyInteractables() && Input.Character.Interact.WasPressedThisFrame())
            {
                print("Sd");
                nearbyInteractables[0].Activate();
                OnInteraction?.Invoke();
            }
        }

    }
}