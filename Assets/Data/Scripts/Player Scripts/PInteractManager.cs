using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace J
{
    public class PInteractManager : MonoBehaviour
    {
        PlayerManager playerManager;
        [SerializeField]
        InputHandler inputHandler;
        CameraHandler cameraHandler;
        [SerializeField]
        Interactable interactableObject;

        [SerializeField]
        public UIManager uiManager;
        InteractableUI interactableUI;

        public bool canInteract;

        private void Awake()
        {
            uiManager = FindObjectOfType<UIManager>();
            interactableUI = uiManager.GetComponentInChildren<InteractableUI>();
            inputHandler = GetComponent<InputHandler>();
        }

        private void OnTriggerEnter(Collider other)
        {
            interactableObject = other.GetComponent<Interactable>();
            Debug.Log(interactableObject);

            if (interactableObject != null)
            {
                uiManager.interactPopUp.GetComponentInChildren<TextMeshProUGUI>().SetText(interactableObject.interactableText);
                uiManager.interactPopUp.SetActive(true);
                canInteract = true;
            }
        }
        
        public void InteractWithObject()
        {
            Debug.Log("Interacter");
            interactableObject.GetComponent<Interactable>().Interact(this);
            canInteract = false;
        }

        private void OnTriggerExit(Collider other)
        {
            uiManager.interactPopUp.GetComponentInChildren<TextMeshProUGUI>().text =null;
            uiManager.interactPopUp.SetActive(false);
            interactableObject = null;
        }
    }
}