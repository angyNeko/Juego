using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J
{
    public class DisableControls : MonoBehaviour
    {
        [SerializeField]
        public bool movDisabled = true;
        [SerializeField]
        Camera mainCamera;

        [SerializeField]
        GameObject player;

        InputHandler inputHandler;

        private void OnEnable()
        {
            movDisabled = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            inputHandler = other.GetComponentInParent<InputHandler>();

            if (!movDisabled)
                return;

            if (!inputHandler.playerInputActions.PlayerMovement.Movement.enabled)
                return;

            inputHandler.playerInputActions.PlayerMovement.Movement.Disable();
            
            Update();
        }

        private void Update()
        {
            Debug.Log("Camera Rotation: " + mainCamera.transform.rotation.y);
            if (mainCamera.transform.rotation.y >= 0.9f)
            {
                if (!movDisabled)
                    return;

                movDisabled = false;
                inputHandler.playerInputActions.PlayerMovement.Movement.Enable();
            }
        }


    }
}