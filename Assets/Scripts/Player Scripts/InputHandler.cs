using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class InputHandler : MonoBehaviour
    {
        private PlayerInputActions playerInputActions;
        PlayerController playerController;
        public Vector2 movementInput;
        public float verticalInput;
        public float horizontalInput;
        public float moveAmount;
        public bool sprintInput;
        public bool rb_input;
        public bool rt_input;

        private void Awake()
        {
            playerController = GetComponent<PlayerController>();
        }

        private void OnEnable()
        {
            if (playerInputActions == null)
            {
                playerInputActions = new PlayerInputActions();

                playerInputActions.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
                
                playerInputActions.PlayerMovement.Movement.canceled += i => movementInput = Vector2.zero;

                playerInputActions.PlayerActions.Sprint.performed += i => sprintInput = true;
                playerInputActions.PlayerActions.Sprint.canceled += i => sprintInput = false;

                playerInputActions.PlayerActions.Jump.performed += i => playerController.HandleJump();
            }

            playerInputActions.PlayerMovement.Enable();
            playerInputActions.PlayerActions.Enable();
        }

        private void OnDisable()
        {
            playerInputActions.Disable();
        }

        public void HandleAllInputs()
        {
            HandleMovementInput();
            HandleSprintInput();
            HandleAttackInput();
        }

        private void HandleMovementInput()
        {
            verticalInput = movementInput.y;
            horizontalInput = movementInput.x;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
            playerController.HandleMovementAnimation(moveAmount);
        }

        public bool HandleSprintInput()
        {
            return sprintInput;
        }
  
        private void HandleAttackInput()
        {
            playerInputActions.PlayerActions.LightAttack.performed += i => rb_input = true;
            playerInputActions.PlayerActions.HeavyAttack.performed += i => rt_input = true;

            // rb is righ hand
            if (rb_input)
            {
                playerController.HandleAttack("light");
            }

            if (rt_input)
            {
                playerController.HandleAttack("heavy");
            }
        }
        private void LateUpdate()
        {
            rb_input = false;
            rt_input = false;
        }
    }

}