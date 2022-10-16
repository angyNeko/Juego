using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J
{
    public class WeaponPickup : Interactable
    {
        public WeaponItem weapon;

        public override void Interact(PlayerManager playerManager)
        {
            base.Interact(playerManager);
        }

        private void PickUpItem(PlayerManager playerManger)
        {
            PlayerInventory playerInventory;
            PlayerLocomotion playerLocomotion;
            AnimatorHandler animatorHandler;

            playerInventory = playerManger.GetComponent<PlayerInventory>();
            playerLocomotion = playerManger.GetComponent<PlayerLocomotion>();
            animatorHandler = playerManger.GetComponentInChildren<AnimatorHandler>();

            playerLocomotion.rigidbody.velocity = Vector3.zero;
            //animatorHandler.PlayTargetAnimation("Empty", true);  //Plays pick up animation
        }
    }
}