using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J
{
    public class PlayerLocomotion : MonoBehaviour
    {
        #region Imports
        [HideInInspector]
        PlayerController playerController;
        #endregion

        Vector3 moveDirection;
        CapsuleCollider capsuleCollider;
        Transform cameraObject;

        [HideInInspector]
        public Transform myTransform;

        public new Rigidbody characterModel;
        public GameObject normalCamera;

        #region stats
        [Header("Stats")]
        [SerializeField]
        float moveSpeed = 1.5f;
        [SerializeField] 
        float walkSpeed = 1.5f;
        [SerializeField] 
        float runSpeed = 7;
        [SerializeField]
        float sprintSpeed = 20;
        [SerializeField] 
        float jumpVal = 7;
        [SerializeField] 
        float rotationSpeed = 10;
        [SerializeField]
        public bool isSprinting;
        #endregion

        float rollRotation;


        private void Start()
        {
            characterModel = GetComponent<Rigidbody>();
            playerController = GetComponent<PlayerController>();
            cameraObject = Camera.main.transform;
            myTransform = transform;
        }

        #region Movement
        Vector3 normalVector;
        Vector3 targetPosition;

        public void HandleMovement(float delta)
        {
            if (playerController.animator.GetBool("isInteracting"))
                return;

            moveDirection = cameraObject.forward * playerController.inputHandler.vertical;
            moveDirection += cameraObject.right * playerController.inputHandler.horizontal;
            moveDirection.Normalize();
            moveDirection.y = 0;


            float speed = moveSpeed;


            if (playerController.inputHandler.sprintFlag)
            {
                speed = sprintSpeed;
                isSprinting = true;
                moveDirection *= speed;
            }
            else
            {
                moveDirection *= speed;
            }

            Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
            characterModel.velocity = projectedVelocity;

            playerController.HandleMovementAnimation(playerController.inputHandler.moveAmount, isSprinting);

            if (playerController.animatorManager.canRotate)
            {
                HandleRotation(delta);
            }
        }

        public void HandleRollAndSprint(float delta)
        {
            if (playerController.inputHandler.rollFlag)
            {
                moveDirection = cameraObject.forward * playerController.inputHandler.vertical;
                moveDirection += cameraObject.right * playerController.inputHandler.horizontal;

                if (playerController.inputHandler.moveAmount > 0)
                {
                    playerController.animatorManager.PlayTargetAnimation("Roll", true);
                    moveDirection.y = 0;
                    Quaternion rollRotation = Quaternion.LookRotation(moveDirection);
                    myTransform.rotation = rollRotation;
                }
                else
                {
                    playerController.DoAnimation("Backstep", true);
                    //playerController.DoAnimation("OH_Heavy_Attack_1", true);
                }
            }

            

            Debug.Log("myTransform = " + transform.position);
        }

        private void HandleRotation(float delta)
        {
            Vector3 targetDirection = Vector3.zero;
            float moveOverride = playerController.inputHandler.moveAmount;

            targetDirection = cameraObject.forward * playerController.inputHandler.vertical;
            targetDirection += cameraObject.right * playerController.inputHandler.horizontal;

            targetDirection.Normalize();
            targetDirection.y = 0;

            if (targetDirection == Vector3.zero)
            {
                targetDirection = myTransform.forward;
            }

            float rs = rotationSpeed;

            Quaternion tr = Quaternion.LookRotation(targetDirection);
            Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rs * delta);

            myTransform.rotation = targetRotation;
        }

        #endregion
    }
}