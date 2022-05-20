using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J
{
    public class InteractButtonTrigger : MonoBehaviour
    {
        [Header("Game Objects")]
        [SerializeField]
        InteractManager interactManager;
        [SerializeField]
        GameObject respawnPoint;

        [Header("Object Name")]
        [SerializeField]
        string objectName;

        [Header("Flags")]
        [SerializeField]
        bool playerEntered;

        RespawnManager respawnManager;
        PlayerController playerController;
        InputHandler inputHandler;
        bool interactCloned;

        void Start()
        {
            respawnManager = GameObject.Find("Respawn Manager").GetComponent<RespawnManager>();
            interactManager = GameObject.Find("Interact Manager").GetComponent<InteractManager>();
        }

        private void Update()
        {
            if (!playerEntered)
                return;

            interactManager.CloneInteractButton(objectName);

            if (inputHandler.playerInputActions.BaseUI.Interact.enabled)
                inputHandler.playerInputActions.BaseUI.Interact.performed += i => respawnManager.SetRespawn(respawnPoint);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag != "Player")
                return;

            inputHandler = other.GetComponent<InputHandler>();

            playerEntered = true;

            if (!inputHandler.playerInputActions.BaseUI.Interact.enabled)
                inputHandler.playerInputActions.BaseUI.Interact.Enable();
        }

        private void OnTriggerExit(Collider other)
        {
            interactManager.DestroyInteractButton(objectName);
            interactManager.statue = false;

            if (playerController != null)
                playerController.interactNear = false;

            playerEntered = false;

            if (inputHandler.playerInputActions.BaseUI.Interact.enabled)
                inputHandler.playerInputActions.BaseUI.Interact.Disable();
        }
    }
}