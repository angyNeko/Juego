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

        public bool isInteracting;
        float delta;

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
            playerLocomotion.isSprinting = inputHandler.b_input;

            inputHandler.TickInput(delta);
            playerLocomotion.HandleMovement(delta);
            playerLocomotion.HandleRollAndSprint(delta);

            inputHandler.sprintFlag = false;
        }
        #endregion

        #region Usual Movements

        public void DoMovement(float delta)
        {
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
        #endregion

        #region Combat Related
        public void HandleAttack(string attackType)
        {
            if (attackType == "light")
            {
                playerAttacker.HandleLightAttack(playerInventory.rightHandWeapon);
                Debug.Log("Light Attack");
            }

            if (attackType == "heavy")
            {
                playerAttacker.HandleHeavyAttack(playerInventory.rightHandWeapon);
                Debug.Log("Heavy Attack");
            }
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
