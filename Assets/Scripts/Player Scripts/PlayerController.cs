using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J
{
    public class PlayerController : MonoBehaviour
    {
        [HideInInspector]
        public Animator animator;
        public PlayerLocomotion playerLocomotion;
        public InputHandler inputHandler;
        public AnimatorManager animatorManager;
        PlayerAttacker playerAttacker;
        PlayerInventory playerInventory;
        PlayerStats playerStats;
        public CameraHandler cameraHandler;

        public bool isInteracting;
        float delta;

        #region Unity Built-in

        private void Awake()
        {
            cameraHandler = CameraHandler.singleton;
        }

        private void Start()
        {
            inputHandler = GetComponent<InputHandler>();
            playerLocomotion = GetComponent<PlayerLocomotion>();

            animator = GetComponentInChildren<Animator>();
            animatorManager = GetComponentInChildren<AnimatorManager>();

            playerInventory = GetComponent<PlayerInventory>();
            playerAttacker = GetComponent<PlayerAttacker>();
            playerStats = GetComponent<PlayerStats>();
            animatorManager.Initialize();
        }

        private void Update()
        {

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

        public void HandleMovementAnimation(float moveAmount)
        {
            animatorManager.UpdateAnimatorValues(0, moveAmount, false);
        }

        public void HandleRoll()
        {
            if (animatorManager.anim.GetBool("isInteracting"))
                return;
            
            playerLocomotion.DoRoll(inputHandler.moveAmount, inputHandler.vertical, inputHandler.horizontal);
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
