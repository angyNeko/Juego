using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    Animator animator;
    private PlayerController playerController;
    private PlayerInputManager inputManager;

    public bool isInteracting;

    private void Awake()
    {
        inputManager = GetComponent<PlayerInputManager>();
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        inputManager.handleAllInputs();
    }

    private void FixedUpdate()
    {
        playerController.handleAllMovements();
    }
}
