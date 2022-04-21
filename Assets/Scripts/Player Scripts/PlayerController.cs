using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerController : MonoBehaviour
    {
        Animator animator;
        PlayerLocomotion playerLocomotion;
        InputHandler inputHandler;
        PlayerAttacker playerAttacker;
        PlayerInventory playerInventory;
        AnimatorManager animatorManager;
        PlayerStats playerStats;

        public bool isInteracting;

        private void Awake()
        {
            inputHandler = GetComponent<InputHandler>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
            animator = GetComponent<Animator>();
            playerInventory = GetComponent<PlayerInventory>();
            animatorManager = GetComponent<AnimatorManager>();
            playerAttacker = GetComponent<PlayerAttacker>();
            playerStats = GetComponent<PlayerStats>();
        }

        private void Update()
        {
            inputHandler.HandleAllInputs();
        }

        private void FixedUpdate()
        {
            playerLocomotion.HandleAllMovements(inputHandler.verticalInput, inputHandler.horizontalInput, inputHandler.moveAmount, 
            inputHandler.sprintInput);
        }

        public void HandleMovementAnimation(float moveAmount)
        {
            animatorManager.UpdateAnimatorValues(0, moveAmount, inputHandler.sprintInput);
        }

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

        public void HandleJump()
        {
            //animatorManager.PlayTargetAnimation("Jump", true);
            playerLocomotion.Jump();
        }

    }
}
