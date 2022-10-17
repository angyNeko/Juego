using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J
{
    public class InputHandler : MonoBehaviour
    {
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;

        public bool sprint_Input;
        public bool lightAtk_Input;
        public bool heavyAtk_Input;
        public bool interact_Input;
        public bool inventory_Input;

        public bool d_Pad_Up;
        public bool d_Pad_Right;
        public bool d_Pad_Down;
        public bool d_Pad_Left;

        public bool dodgelFlag;
        public bool sprintFlag;
        public bool comboFlag;
        public bool inventoryFlag;
        public float rollInputTimer;

        PlayerInputActions inputActions;
        PlayerAttacker playerAttacker;
        PlayerInventory playerInventory;
        PlayerManager playerManager;
        UIManager uiManager;

        Vector2 movementInput;
        Vector2 cameraInput;

        private void Start()
        {
            playerAttacker = GetComponent<PlayerAttacker>();
            playerInventory = GetComponent<PlayerInventory>();
            playerManager = GetComponent<PlayerManager>();
            uiManager = FindObjectOfType<UIManager>();
        }

        public void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerInputActions();
                inputActions.PlayerMovement.Movement.performed += inputAction => movementInput = inputAction.ReadValue<Vector2>();
                inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            }

            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        public void TickInput(float delta)
        {
            MoveInput(delta);
            HandleRollSprintInput(delta);
            HandleAttackInput(delta);
            HandleQuickSlotInput();
            HandleInteractInput();
            //HandleJumpInput();
            HandleInventoryInput();
        }

        public void ResetBools()
        {
            dodgelFlag = false;
            sprintFlag = false;
            comboFlag = false;

            sprint_Input = false;
            lightAtk_Input = false;
            heavyAtk_Input = false;
            interact_Input = false;
            inventory_Input = false;

            d_Pad_Right = false;
            d_Pad_Left = false;
            d_Pad_Up = false;
            d_Pad_Down = false;
        }

        private void MoveInput(float delta)
        {
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }

        private void HandleRollSprintInput(float delta)
        {
            sprintFlag = inputActions.PlayerActions.Sprint.phase == UnityEngine.InputSystem.InputActionPhase.Started;
            inputActions.PlayerActions.Dodge.performed += i => dodgelFlag = true;

            if (sprintFlag)
            {
                sprintFlag = true;
            }
        }

        private void HandleAttackInput(float delta)
        {
            inputActions.PlayerActions.LightAttack.performed += i => lightAtk_Input = true;
            inputActions.PlayerActions.HeavyAttack.performed += i => heavyAtk_Input = true;

            if (lightAtk_Input)
            {
                if (playerManager.canDoCombo)
                {
                    comboFlag = true;
                    playerAttacker.HandleWeaponCombo(playerInventory.rightHandWeapon);
                    comboFlag = false;
                }
                else if (playerManager.isInteracting && !playerManager.canDoCombo)
                {
                    return;
                }
                else
                {
                    playerAttacker.HandleLightAttack(playerInventory.rightHandWeapon);
                }
            }

            if (heavyAtk_Input)
            {
                return;
                //playerAttacker.HandleHeavyAttack(playerInventory.rightHandWeapon);
            }
            
        }

        private void HandleQuickSlotInput()
        {
            inputActions.PlayerQuickSlots.DPadRight.performed += i => d_Pad_Right = true;
            inputActions.PlayerQuickSlots.DPadLeft.performed += i => d_Pad_Left = true;

            if (d_Pad_Right)
            {
                playerInventory.ChangeRightHandWeapon();
            }
            if (d_Pad_Left)
            {
                //playerInventory.ChangeRightHandWeapon;
            }
        }

        private void HandleInteractInput()
        {
            inputActions.PlayerActions.Interact.performed += i => interact_Input = true;
        }

        private void HandleJumpInput()
        {

        }

        private void HandleInventoryInput()
        {
            inputActions.PlayerActions.Inventory.performed += inputActions => inventory_Input = true;

            if (inventory_Input)
            {
                inventoryFlag = !inventoryFlag; 

                if (inventoryFlag)
                {
                    uiManager.OpenSelectWindow(); 
                    uiManager.UpdateUI();
                    uiManager.hudWindow.SetActive(false);
                }
                else
                {
                    uiManager.CloseSelectWindow();
                    uiManager.CloseAllInventoryWindows();
                    uiManager.hudWindow.SetActive(true);
                }
            }
        }
    }
}