using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private PlayerController playerController;
    AnimatorManager animatorManager;

    public Vector2 movementInput;
    public float verticalInput;
    public float horizontalInput;
    public float moveAmount;
    public bool sprintInput;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        playerController = GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        if (playerInputActions == null)
        {
            playerInputActions = new PlayerInputActions();

            playerInputActions.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            
            playerInputActions.PlayerMovement.Movement.canceled += i => movementInput = Vector2.zero;

            playerInputActions.PlayerActions.Sprint.performed += i => sprintInput = true;
            playerInputActions.PlayerActions.Sprint.canceled += i => sprintInput = false;

            if (sprintInput == true)
            {
                playerController.isSprinting = true;
                Debug.Log("Sprint True");
            }
        }

        playerInputActions.PlayerMovement.Enable();
        playerInputActions.PlayerActions.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }

    public void handleAllInputs()
    {
        handleMovementInput();
        handleSprintInput();
    }

    private void handleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, moveAmount, playerController.isSprinting);
    }

    private void handleSprintInput()
    {
        if (sprintInput && moveAmount > 0.55f)
        {
            playerController.isSprinting = true;
        }
        else
        {
            playerController.isSprinting = false;
        }
    }
}
