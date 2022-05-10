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
        }

        private void OnEnable()
        {
            if (isInTutorial)
                return;

            if (playerInputActions == null)
            {
                playerInputActions = new PlayerInputActions();
                playerInputActions.PlayerMovement.Movement.performed += playerInputActions => movementInput = playerInputActions.ReadValue<Vector2>();
                playerInputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            }

            playerInputActions.Enable();
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
            //HandleAttackInput(delta);
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
            b_input = playerInputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Started;

            if (b_input)
            {
                rollInputTimer += delta;
                sprintFlag = true;
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