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
        public bool lightAtkInput;
        public bool heavyAtkInput;
        public bool d_Pad_Up;
        public bool d_Pad_Right;
        public bool d_Pad_Down;
        public bool d_Pad_Left;


        public bool rollFlag;
        public bool sprintFlag;
        public bool comboFlag;
        public float rollInputTimer;

        PlayerInputActions inputActions;
        PlayerAttacker playerAttacker;
        PlayerInventory playerInventory;
        PlayerManager playerManager;

        Vector2 movementInput;
        Vector2 cameraInput;

        private void Start()
        {
            playerAttacker = GetComponent<PlayerAttacker>();
            playerInventory = GetComponent<PlayerInventory>();
            playerManager = GetComponent<PlayerManager>();
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
            HandleRollInput(delta);
            HandleAttackInput(delta);
            HandleQuickSlotInput(delta);
        }

        public void ResetBools()
        {
            rollFlag = false;
            sprintFlag = false;
            sprint_Input = false;
            lightAtkInput = false;
            heavyAtkInput = false;
            comboFlag = false;
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

        private void HandleRollInput(float delta)
        {
            sprint_Input = inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Started;
            
            if (sprint_Input)
            {
                rollInputTimer += delta;
                sprintFlag = true;
            }
            else
            {
                if (rollInputTimer > 0 && rollInputTimer < 0.5f)
                {
                    sprintFlag = false;
                    rollFlag = true;
                }

                rollInputTimer = 0;
            }
        }

        private void HandleAttackInput(float delta)
        {
            inputActions.PlayerActions.LightAttack.performed += i => lightAtkInput = true;
            inputActions.PlayerActions.HeavyAttack.performed += i => heavyAtkInput = true;

            if (lightAtkInput)
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

            if (heavyAtkInput)
            {
                return;
                //playerAttacker.HandleHeavyAttack(playerInventory.rightHandWeapon);
            }
            
        }

        private void HandleQuickSlotInput(float delta)
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
    }
}