using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerLocomotion : MonoBehaviour
    {
        // Imports
        Rigidbody characterModel;
        CapsuleCollider capsuleCollider;

        // Cant modify
        Vector3 moveDirection;
        Transform cameraObject;

        public float distanceToGround = 0.625f;

        [Header("Movement Flags")]
        //public bool sprintActive;

        [Header("Movement Speeds")]
        [SerializeField] float walkSpeed = 1.5f;
        [SerializeField] float runSpeed = 7;
        [SerializeField] float sprintSpeed = 20;
        [SerializeField] float jumpVal = 7;
        [SerializeField] float rotationSpeed = 5;

        private float distToGround;
        public LayerMask groundlayer;
        

        private void Awake()
        {
            characterModel = GetComponent<Rigidbody>();
            cameraObject = Camera.main.transform;
            capsuleCollider = GetComponent<CapsuleCollider>();
        }

        private void Start()
        {
            distanceToGround = capsuleCollider.bounds.extents.y;
        }

        public void HandleAllMovements(float verticalInput, float horizontalInput, float moveAmount, bool sprintActive)
        {
            HandleMovement(verticalInput, horizontalInput, moveAmount, sprintActive);
            HandleRotation(verticalInput, horizontalInput);
        }

        private void HandleMovement(float verticalInput, float horizontalInput, float moveAmount, bool sprintActive)
        {
            HandleFalling();

            moveDirection = cameraObject.forward * verticalInput;
            moveDirection = moveDirection + cameraObject.right * horizontalInput;
            moveDirection.Normalize();
            moveDirection.y = 0;

            if (sprintActive)
            {
                moveDirection = moveDirection * sprintSpeed;
                Debug.Log("Sprinting");
            }
            else{

                if (moveAmount >= 0.5f)
                {
                    moveDirection = moveDirection * runSpeed;
                }
                else
                {
                    moveDirection = moveDirection * walkSpeed;
                }
                
            }

            Vector3 movementVelocity = moveDirection;

            characterModel.velocity = movementVelocity;

            Debug.Log("movementVelocity: " + movementVelocity);
        }

        public void Jump()
        {
            characterModel.AddForce(Vector3.up * jumpVal);
        }

        private void HandleRotation(float verticalInput, float horizontalInput)
        {
            Vector3 targetDirection = Vector3.zero;

            targetDirection = cameraObject.forward * verticalInput;
            targetDirection = targetDirection + cameraObject.right * horizontalInput;
            targetDirection.Normalize();
            targetDirection.y = 0;

            if (targetDirection == Vector3.zero)
            {
                targetDirection = transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            transform.rotation = playerRotation;
        }

        // Ground Check and Fall Handling
        bool IsGrounded()
        {
            return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f, groundlayer, QueryTriggerInteraction.Collide);
        }

        private void HandleFalling()
        {
            if (!IsGrounded())
            {
                characterModel.AddForce(Vector3.down * 9.8f);
            }
        }

    }
}