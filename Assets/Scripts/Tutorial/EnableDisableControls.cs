using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J
{
    public class EnableDisableControls : MonoBehaviour
    {
        public Cinemachine.CinemachineInputProvider inputProvider;
        InputHandler inputHandler;

        bool playerMovedCamera;
        Vector2 cameraInput;

        float yInputCamera;

        private void OnTriggerEnter(Collider other)
        {
            inputHandler = other.GetComponent<InputHandler>();

            if (inputHandler != null)
            {
                EnableCamera();
            }
        }

        private void EnableCamera()
        {
            inputHandler.isInTutorial = true;
            inputHandler.playerInputActions.PlayerMovement.Camera.Enable();
            inputHandler.playerInputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
        }
    }
}
