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

        [Header("PlayerVel")]
        [SerializeField]
        float x;
        [SerializeField]
        float y;

        public Vector3 moveDirection;
        CapsuleCollider capsuleCollider;
        Transform cameraObject;

        [HideInInspector]
        public Transform myTransform;

        public new Rigidbody characterModel;
        public GameObject normalCamera;

        #region stats
        [Header("Ground & Air Detection Stats")]
        [SerializeField]
        float groundDetectionRayStartPoint = 0.5f;
        [SerializeField]
        float minDistanceToBeginFall = 1f;
        [SerializeField]
        float groundDirectionRayDistance = 0.2f;
        LayerMask ignoreForGroundCheck;
        public float inAirTimer;
        [SerializeField]
        float maxDist;

        [Header("Movement Stats")]
        [SerializeField]
        float moveSpeed = 1.5f;
        [SerializeField] 
        float walkSpeed = 1.5f;
        [SerializeField] 
        float runSpeed = 7;
        [SerializeField]
        float sprintSpeed = 20;
        [SerializeField]
        float fallSpeed = 45;
        [SerializeField]
        float jumpVal = 7;
        [SerializeField] 
        float rotationSpeed = 10;
        #endregion

        float rollRotation;


        private void Start()
        {
            characterModel = GetComponent<Rigidbody>();
            playerController = GetComponent<PlayerController>();
            cameraObject = Camera.main.transform;
            myTransform = transform;

            playerController.isGrounded = true;
            ignoreForGroundCheck = ~(1 << 8 | 1 << 11);
        }

        #region Movement
        Vector3 normalVector;
        Vector3 targetPosition;

        public void HandleMovement(float delta)
        {
            if (playerController.inputHandler.rollFlag)
                return;
            if (playerController.animator.GetBool("isInteracting"))
                return;

            moveDirection = cameraObject.forward * playerController.inputHandler.vertical;
            moveDirection += cameraObject.right * playerController.inputHandler.horizontal;
            moveDirection.Normalize();
            moveDirection.y = 0;

            float speed = moveSpeed;


            if (playerController.isSprinting)
            {
                speed = sprintSpeed;
                moveDirection *= speed;
            }

            else
            {
                moveDirection *= speed;
            }

            Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
            characterModel.velocity = projectedVelocity;

            playerController.HandleMovementAnimation(playerController.inputHandler.moveAmount, playerController.isSprinting);

            x = projectedVelocity.x;
            y = projectedVelocity.y;

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

        public void HandleFallingAnimation(float delta, Vector3 moveDirection)
        {
            playerController.isGrounded = true;
            RaycastHit hit;
            Vector3 origin = myTransform.position;
            origin.y += groundDetectionRayStartPoint;

            if (Physics.Raycast(origin, myTransform.forward, out hit, maxDist))
            {
                moveDirection = Vector3.zero;
            }

            if (playerController.isInAir)
            {
                characterModel.AddForce(-Vector3.up * fallSpeed);
                characterModel.AddForce(moveDirection * fallSpeed / 10f);
            }

            Vector3 dir = moveDirection;
            dir.Normalize();
            origin = origin + dir * groundDirectionRayDistance;

            targetPosition = myTransform.position;

            Debug.DrawRay(origin, -Vector3.up * groundDetectionRayStartPoint, Color.yellow, 2, false);
            //Debug.DrawRay(origin, -Vector3.up * minDistanceToBeginFall, Color.red, 0.3f, false);
            if (Physics.Raycast(origin, -Vector3.up, out hit, minDistanceToBeginFall, ignoreForGroundCheck))
            {
                normalVector = hit.normal;
                Vector3 tp = hit.point;
                playerController.isGrounded = true;
                targetPosition.y = tp.y;

                if (playerController.isInAir)
                {
                    if (inAirTimer > 0.5f)
                    {
                        Debug.Log("Air Time: " + inAirTimer);
                        playerController.DoAnimation("Landing", true);
                    }
                    else
                    {
                        playerController.DoAnimation("Empty", false);
                        inAirTimer = 0;
                    }

                    playerController.isInAir = false;
                }
            }
            else
            {
                if (playerController.isGrounded)
                {
                    playerController.isGrounded = false;
                }

                if (playerController.isInAir == false)
                {
                    if (playerController.isInteracting == false)
                    {
                        playerController.DoAnimation("Falling", true);
                    }

                    Vector3 vel = characterModel.velocity;
                    vel.Normalize();
                    characterModel.velocity = vel * (moveSpeed / 2);
                    playerController.isInAir = true;
                }
            }

            if (playerController.isInteracting || playerController.inputHandler.moveAmount > 0)
            {
                myTransform.position = Vector3.Lerp(myTransform.position, targetPosition, Time.deltaTime / 0.1f);
            }
            else
            {
                myTransform.position = targetPosition;
            }
        }

        #endregion
    }
}