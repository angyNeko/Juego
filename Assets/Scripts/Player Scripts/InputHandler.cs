using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class InputHandler : MonoBehaviour
    {
        Animator animator;
        private PlayerLocomotion playerLocomotion;
        private InputManager inputManager;

        public bool isInteracting;

        private void Awake()
        {
            inputManager = GetComponent<InputManager>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            inputManager.HandleAllInputs();
        }

        private void FixedUpdate()
        {
            playerLocomotion.HandleAllMovements();
        }

        private void LateUpdate()
        {
            inputManager.rb_input = false;
            inputManager.rt_input = false;
        }
    }
}
