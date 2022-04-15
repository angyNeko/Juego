using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerInputManager : MonoBehaviour
    {
        private PlayerInputActions playerInputActions;
        private PlayerController playerController;
        AnimatorManager animatorManager;
        PlayerAttacker playerAttacker;
        PlayerInventory playerInventory;

        public Vector2 movementInput;
        public float verticalInput;
        public float horizontalInput;
        public float moveAmount;
        public bool sprintInput;
        public bool rb_input;
        public bool rt_input;

        private void Awake()
        {
            animatorManager = GetComponent<AnimatorManager>();
            playerController = GetComponent<PlayerController>();
            playerAttacker = GetComponent<PlayerAttacker>();
            playerInventory = GetComponent<PlayerInventory>();
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

                if (sprintInput == true)
                {
                    playerController.isSprinting = true;
                    Debug.Log("Sprint True");
                }
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
            animatorManager.UpdateAnimatorValues(0, moveAmount, playerController.isSprinting);
        }

        private void HandleSprintInput()
        {
            if (sprintInput && moveAmount > 0.55f)
            {
                playerController.isSprinting = true;
            }
            else
            {
                playerController.isSprinting = false;
            }
        }
    
        private void HandleAttackInput()
        {
            playerInputActions.PlayerActions.LightAttack.performed += i => rb_input = true;
            playerInputActions.PlayerActions.HeavyAttack.performed += i => rt_input = true;

            // rb is righ hand
            if (rb_input)
            {
                playerAttacker.HandleLightAttack(playerInventory.rightHandWeapon);
                Debug.Log("Light Attack");
            }

            if (rt_input)
            {
                playerAttacker.HandleHeavyAttack(playerInventory.rightHandWeapon);
                Debug.Log("Heavy Attack");
            }
        }
    }

}