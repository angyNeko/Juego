using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace J
{
    public class WeaponPickup : Interactable
    {
        public WeaponItem weapon;

        public override void Interact(PlayerManager playerManager)
        {
            base.Interact(playerManager);

            PickUpItem(playerManager);
        }

        private void PickUpItem(PlayerManager playerManger)
        {
            PlayerInventory playerInventory;
            PlayerLocomotion playerLocomotion;
            AnimatorHandler animatorHandler;

            playerInventory = playerManger.GetComponent<PlayerInventory>();
            playerLocomotion = playerManger.GetComponent<PlayerLocomotion>();
            animatorHandler = playerManger.GetComponentInChildren<AnimatorHandler>();

            playerLocomotion.rigidbody.velocity = Vector3.zero; //Stops player from moving while picking up item
            //animatorHandler.PlayTargetAnimation("Empty", true);  //Plays pick up animation
            playerInventory.weaponsInventory.Add(weapon);
            playerManger.itemInteractableGameObject.GetComponentInChildren<TextMeshProUGUI>().text = weapon.itemName;
            playerManger.itemInteractableGameObject.GetComponentInChildren<RawImage>().texture = weapon.itemIcon.texture;
            playerManger.itemInteractableGameObject.SetActive(true);
            Debug.Log(weapon.itemName);
            Destroy(gameObject);
        }
    }
}