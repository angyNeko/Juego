using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace J
{
    public class PlayerManager : CharacterManager
    {
        InputHandler inputHandler;
        PlayerLocomotion playerLocomotion;
        Animator anim;
        public CameraHandler cameraHandler;
        public GameObject cameraOverride;

        public PInteractManager interactManager;

        [Header("Player Flags")]
        public bool isInteracting;
        public bool isSprinting;
        public bool isInAir;
        public bool isGrounded;
        public bool canDoCombo;
        public bool isKnockedBack;

        /*public List<Collider> Ragdallpart = new List<Collider>();*/
        private void Awake()
        {
            InitializeCamera();
            /*SetRagdall();*/
        }

        void Start()
        {
            inputHandler = GetComponent<InputHandler>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
            anim = GetComponentInChildren<Animator>();
            interactManager = GetComponent<PInteractManager>();
        }

        //Here
        void Update()
        {
            float delta = Time.deltaTime;
            isInteracting = anim.GetBool("isInteracting");
            canDoCombo = anim.GetBool("canDoCombo");

            isSprinting = inputHandler.sprint_Input;
            inputHandler.TickInput(delta);
            playerLocomotion.HandleMovement(delta);
            playerLocomotion.HandleRollAndSprint(delta);

        }

        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;

            playerLocomotion.HandleFalling(delta, playerLocomotion.moveDirection);

            if (cameraHandler != null)
            {
                cameraHandler.FollowTarget(delta, inputHandler.vertical);
                cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
            }
            else
                InitializeCamera();
        }

        // Reset flags here
        private void LateUpdate()
        {
            if (isInAir)
            {
                playerLocomotion.inAirTimer = playerLocomotion.inAirTimer + Time.deltaTime;
            }

            inputHandler.ResetBools();
        }

        public void InitializeCamera()
        {
            cameraHandler = FindObjectOfType<CameraHandler>();
        }

        

        public void DisplayInteractableObject()
        {

        }

        private void isTriggerRagdall()
        {

        }
    }
}