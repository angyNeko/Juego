using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace J
{
    public class PlayerLocomotionCC : MonoBehaviour
    {
        Transform cameraObject;
        InputHandlerCC inputHandler;
        public Vector3 moveDirection;

        [HideInInspector]
        public Transform myTransform;
        [HideInInspector]
        public AnimatorHandlerCC animatorHandler;
        [HideInInspector]
        public PlayerManagerCC playerManager;

        public CharacterController characterController;
        public GameObject normalCamera;

        [Header("Ground and Air Detection Stats")]
        [SerializeField]
        float groundDetectionRayStartPoint = 0.35f;
        [SerializeField]
        float minimumDistanceNeededToBeginFall = 1f;
        [SerializeField]
        // Offset raycast
        float groundDirectionRayDistance = 0.35f;
        LayerMask ignoreForGroundCheck;
        public float inAirTimer;

        [Header("Movement   Stats")]
        [SerializeField]
        float movementSpeed = 5;
        [SerializeField]
        float sprintSpeed = 7;
        [SerializeField]
        float rotationSpeed = 10;
        [SerializeField]
        float fallingSpeed = 45f;

        // Start is called before the first frame update
        void Start()
        {
            inputHandler = GetComponent<InputHandlerCC>();
            animatorHandler = GetComponent<AnimatorHandlerCC>();
            playerManager = GetComponent<PlayerManagerCC>();
            characterController = GetComponent<CharacterController>();
            cameraObject = Camera.main.transform;
            myTransform = transform;
            animatorHandler.Initialize();

            ignoreForGroundCheck = ~(1 << 8 | 1 << 11);
        }

        #region Movement
        Vector3 normalVector;
        Vector3 targetPosition;

        private void HandleRoatation(float delta)
        {
            Vector3 targetDir = Vector3.zero;
            float moveOverride = inputHandler.moveAmount;

            targetDir = cameraObject.forward * inputHandler.vertical;
            targetDir += cameraObject.right * inputHandler.horizontal;

            targetDir.Normalize();
            targetDir.y = 0;

            if (targetDir == Vector3.zero)
                targetDir = myTransform.forward;

            float rs = rotationSpeed;

            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rs * delta);

            myTransform.rotation = targetRotation;
        }

        public void HandleMovement(float delta)
        {
            if (inputHandler.rollFlag)
                return;

            if (playerManager.isInteracting)
                return;

            moveDirection = cameraObject.forward * inputHandler.vertical;
            moveDirection += cameraObject.right * inputHandler.horizontal;
            moveDirection.Normalize();
            moveDirection.y = 0;

            float speed = movementSpeed;

            if (inputHandler.sprintFlag)
            {
                speed = sprintSpeed;
                playerManager.isSprinting = true;
                moveDirection *= speed;
            }
            else
            {
                moveDirection *= speed;
            }

            characterController.SimpleMove(moveDirection);

            animatorHandler.UpdateAnimatorValues(inputHandler.moveAmount, 0, playerManager.isSprinting);

            if (animatorHandler.canRotate)
            {
                HandleRoatation(delta);
            }
        }

        public void HandleRollAndSprint(float delta)
        {
            //if (playerManager.isInteracting == true)
            //    return;

            if (inputHandler.rollFlag)
            {
                moveDirection = cameraObject.forward * inputHandler.vertical;
                moveDirection += cameraObject.right * inputHandler.horizontal;

                if (inputHandler.moveAmount > 0)
                {
                    animatorHandler.PlayTargetAnimation("Roll", true);
                    moveDirection.y = 0;
                    Quaternion rollRotation = Quaternion.LookRotation(moveDirection);
                    myTransform.rotation = rollRotation;
                }
                else
                {
                    animatorHandler.PlayTargetAnimation("Dodging Back", true);
                }
            }
        }

        public void HandleFalling(float delta)
        {
            if (!characterController.isGrounded)
            {
                Vector3 vel = characterController.velocity;
                vel.Normalize();
                characterController.Move(Vector3.down * (movementSpeed / 2));
                playerManager.isInAir = true;
            }
            else
            {
                playerManager.isInAir = false;
            }
        }

        #endregion
    }
}
