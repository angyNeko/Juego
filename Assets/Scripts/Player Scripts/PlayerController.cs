using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Imports
    Rigidbody characterModel;
    PlayerInputManager playerInputManager;
    AnimatorManager animatorManager;
    PlayerInputHandler playerInputHandler;
    CapsuleCollider capsuleCollider;

    // Cant modify
    Vector3 moveDirection;
    Transform cameraObject;

    public float distanceToGround = 0.625f;

    [Header("Falling")]
    [SerializeField] float inAirTimer;
    [SerializeField] float leapingVelocity;
    [SerializeField] float fallingVelocity;
    //[SerializeField] float raycastHeifOffset = 0.5f;
    public LayerMask groundlayer;
    //private bool isGrounded = true;
    private float distToGround;

    [SerializeField] Transform groundCheck;

    [Header("Movement Flags")]
    public bool isSprinting;

    [Header("Movement Speeds")]
    [SerializeField] float walkSpeed = 1.5f;
    [SerializeField] float runSpeed = 7;
    [SerializeField] float sprintSpeed = 20;
    //[SerializeField] float jumpVal = 7;
    [SerializeField] float rotationSpeed = 5;

    private void Awake()
    {
        characterModel = GetComponent<Rigidbody>();
        playerInputManager = GetComponent<PlayerInputManager>();
        animatorManager = GetComponent<AnimatorManager>();
        playerInputHandler = GetComponent<PlayerInputHandler>();
        cameraObject = Camera.main.transform;
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void Start()
    {
        distanceToGround = capsuleCollider.bounds.extents.y;
    }

    public void handleAllMovements()
    {
        handleMovement();
        handleRotation();
    }

    private void handleMovement()
    {
        handleFalling();

        moveDirection = cameraObject.forward * playerInputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * playerInputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;

        if (isSprinting)
        {
            moveDirection = moveDirection * sprintSpeed;
            Debug.Log("Sprinting");
        }
        else{

            if (playerInputManager.moveAmount >= 0.5f)
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

    private void handleRotation()
    {
        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * playerInputManager.verticalInput;
        targetDirection = targetDirection + cameraObject.right * playerInputManager.horizontalInput;
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

    private void handleFalling()
    {

        Debug.Log("Grounded: " + IsGrounded());
        if (!IsGrounded())
        {
            
        }
    }

}
