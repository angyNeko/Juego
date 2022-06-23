using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J
{
    public class PlayerController : MonoBehaviour
    {
        [HideInInspector]
        public Animator animator;
        [HideInInspector]
        public PlayerLocomotion playerLocomotion;
        [HideInInspector]
        public InputHandler inputHandler;
        [HideInInspector]
        public AnimatorManager animatorManager;
        [HideInInspector]
        PlayerAttacker playerAttacker;
        [HideInInspector]
        PlayerInventory playerInventory;
        [HideInInspector]
        PlayerStats playerStats;
        [HideInInspector]
        public CameraHandler cameraHandler;

        [Header("Player Flags")]
        public bool isInteracting;
        public bool isSprinting;
        public bool isInAir;
        public bool isGrounded;

        public bool dialogued;
        public bool interactNear;

        public Transform playerCTransform;

        #region Unity Built-in

        private void Awake()
        {
            cameraHandler = CameraHandler.singleton;
        }

        private void Start()
        {
            inputHandler = GetComponent<InputHandler>();
            playerLocomotion = GetComponent<PlayerLocomotion>();

            animator = GetComponent<Animator>();
            animatorManager = GetComponent<AnimatorManager>();

            playerInventory = GetComponent<PlayerInventory>();
            playerAttacker = GetComponent<PlayerAttacker>();
            playerStats = GetComponent<PlayerStats>();
            animatorManager.Initialize();
        }

        private void Update()
        {
            float delta = Time.deltaTime;

            inputHandler.isInteracting = animator.GetBool("isInteracting");
            inputHandler.rollFlag = false;
            isSprinting = inputHandler.b_input;

            inputHandler.TickInput(delta);

            playerLocomotion.HandleMovement(delta);
            playerLocomotion.HandleRollAndSprint(delta);
            playerLocomotion.HandleFallingAnimation(delta, playerLocomotion.moveDirection) ;

            inputHandler.sprintFlag = inputHandler.b_input;
        }

        private void FixedUpdate()
        {
            if (dialogued)
            {
                inputHandler.horizontal = 0f;
                inputHandler.vertical = 0f;
                inputHandler.moveAmount = 0f;
                return;
            }

            isInteracting = animator.GetBool("isInteracting");
        }

        private void LateUpdate()
        {
            inputHandler.rollFlag = false;
            inputHandler.sprintFlag = false;
            inputHandler.rb_input = false;
            inputHandler.rt_input = false;
            
            if (isInAir)
            {
                playerLocomotion.inAirTimer += Time.deltaTime;
            }
        }
        #endregion

        #region Usual Movements

        public void DoMovement(float delta)
        {
            if (dialogued)
            {
                inputHandler.vertical = 0;
                inputHandler.horizontal = 0;
                return;
            }

            playerLocomotion.HandleMovement(delta);
        }

        public void DoCamera(float fdelta)
        {
            cameraHandler.FollowTarget(fdelta);
            cameraHandler.HandleCameraRotation(fdelta, inputHandler.cameraX, inputHandler.cameraY);

        }

        public void HandleMovementAnimation(float moveAmount, bool isInteracting)
        {
            animatorManager.UpdateAnimatorValues(0, moveAmount, false);
        }

        public void DoAnimation(string targetAnimation, bool isInteracting)
        {
            animatorManager.PlayTargetAnimation(targetAnimation, isInteracting);
        }

        public void DoRoll(float delta)
        {
            if (animatorManager.anim.GetBool("isInteracting") == true)
                return;
            
            playerLocomotion.HandleRollAndSprint(delta);
        }        

        public void HandleDamage(int damage)
        {
            animatorManager.PlayTargetAnimation("Damage", true);
            playerStats.TakeDamage(damage);
        }

        public void HandleDeath()
        {
            animatorManager.PlayTargetAnimation("Dead", true);
        }
        #endregion

    }
}
