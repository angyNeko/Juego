using J;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J
{
    public class PlayerManagerCC : MonoBehaviour
    {
        InputHandlerCC inputHandler;
        PlayerLocomotionCC playerLocomotion;
        Animator anim;
        public CameraHandler cameraHandler;

        [Header("Player Flags")]
        public bool isInteracting;
        public bool isSprinting;
        public bool isInAir;
        public bool isGrounded;

        private void Awake()
        {
            InitializeCamera();
        }

        // Start is called before the first frame update
        void Start()
        {
            inputHandler = GetComponent<InputHandlerCC>();
            playerLocomotion = GetComponent<PlayerLocomotionCC>();
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            float delta = Time.deltaTime;
            isInteracting = anim.GetBool("isInteracting");
            isSprinting = inputHandler.bInput;
            inputHandler.TickInput(delta);
            playerLocomotion.HandleRollAndSprint(delta);
            playerLocomotion.HandleFalling(delta);
            isGrounded = playerLocomotion.characterController.isGrounded;
        }

        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;
            playerLocomotion.HandleMovement(delta);

            if (cameraHandler != null)
            {
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
            }
            else
                InitializeCamera();
        }

        // Reset flags here
        private void LateUpdate()
        {
            inputHandler.rollFlag = false;
            inputHandler.sprintFlag = false;
            isSprinting = inputHandler.bInput;

            if (isInAir)
            {
                playerLocomotion.inAirTimer = playerLocomotion.inAirTimer + Time.deltaTime;
            }

        }

        public void InitializeCamera()
        {
            cameraHandler = CameraHandler.singleton;
        }
    }
}
