using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
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
            inputManager.HandleAllInputs();
        }

        private void FixedUpdate()
        {
            playerController.handleAllMovements();
        }

        private void LateUpdate()
        {
            inputManager.rb_input = false;
            inputManager.rt_input = false;
        }
    }
}
