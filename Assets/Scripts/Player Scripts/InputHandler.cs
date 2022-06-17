using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace J
{
    public class InputHandler : MonoBehaviour
    {
        public PlayerInputActions playerInputActions;
        PlayerAttacker playerAttacker;
        PlayerInventory playerInventory;
        PlayerController playerController;
        [SerializeField]
        DialogueManager dialogueManager;
        public Cinemachine.CinemachineInputProvider cinemachineInputProvider;

        public bool isInTutorial;

        public Vector2 movementInput;
        public Vector2 cameraInput;

        public float vertical;
        public float horizontal;
        public float moveAmount;
        public float cameraX;
        public float cameraY;
        
        [Header("Input buttons")]
        public bool rb_input;
        public bool rt_input;
        public bool b_input;

        [Header("Flags")]
        public bool rollFlag;
        public bool sprintFlag;
        public float rollInputTimer;
        public bool isInteracting;

        public bool isInUI;
        public Button nextButton;

        private void Awake()
        {
            playerController = GetComponent<PlayerController>();
            playerAttacker = GetComponent<PlayerAttacker>();
            playerInventory = GetComponent<PlayerInventory>();
        }

        private void OnEnable()
        {
            if (playerInputActions == null)
            {
                playerInputActions = new PlayerInputActions();
                playerInputActions.PlayerMovement.Movement.performed += playerInputActions => movementInput = playerInputActions.ReadValue<Vector2>();
                playerInputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            }

            playerInputActions.Enable();


            if (isInTutorial)
                OnDisable();
        }

        public void OnDisable()
        {
            playerInputActions.Disable();
        }

        private void Update()
        {
            float delta = Time.deltaTime;

            if (isInUI)
            {
                UIInputs();
                cinemachineInputProvider.enabled = false;
                return;
            }

            TickInput(delta);
            cinemachineInputProvider.enabled = true;

            // Experimental
            GetComponent<CapsuleCollider>().enabled = !isInteracting;
        }

        private void FixedUpdate()
        {
            float fdelta = Time.fixedDeltaTime;

        }

        public void TickInput(float delta)
        {
            if (isInUI)
                return;

            HandleMoveInput(delta);
            HandleRollInput(delta);
            HandleAttackInput(delta);
        }

        #region Movements
        private void HandleMoveInput(float delta)
        {
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));

            cameraX = cameraInput.x;
            cameraY = cameraInput.y;

            playerController.DoMovement(delta);
        }

        private void HandleRollInput(float delta)
        {
            if (!playerInputActions.PlayerActions.Roll.enabled)
                return;

            playerInputActions.PlayerActions.Roll.started += i => b_input = true;
            playerInputActions.PlayerActions.Roll.canceled += i => b_input = false;

            if (b_input && moveAmount > 0.5f)
            {
                rollInputTimer += delta;
                sprintFlag = true;
                playerController.animatorManager.UpdateAnimatorValues(horizontal, vertical, true);
            }

            else
            {
                if (rollInputTimer > 0 && rollInputTimer < 0.5)
                {
                    sprintFlag = false;
                    rollFlag = true;
                    playerController.DoRoll(delta);
                    GetComponent<CapsuleCollider>().enabled = false;
                }

                rollInputTimer = 0;
            }
        }
  
        private void HandleAttackInput(float delta)
        {
            playerInputActions.PlayerActions.RB.performed += i => rb_input = true;
            playerInputActions.PlayerActions.RT.performed += i => rt_input = true;

            // rt is left hand
            if (rt_input)
            {
                playerAttacker.HandleHeavyAttack(playerInventory.leftHandWeapon);
            }

            if (rb_input)
            {
                playerAttacker.HandleLightAttack(playerInventory.rightHandWeapon);
            }

        }

        #endregion
        
        private void LateUpdate()
        {
            rb_input = false;
            rt_input = false;
        }

        #region Ui

        public void UIInputs()
        {
            playerInputActions.UI.NextButton.performed += i => HandleUINextInput();
        }
        public void HandleUINextInput()
        {
            dialogueManager.DisplayNextSentence();
        }
        #endregion
    }

}