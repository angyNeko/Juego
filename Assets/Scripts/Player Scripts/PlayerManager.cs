using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J
{
    public class PlayerManager : MonoBehaviour
    {
        InputHandler inputHandler;
        Animator anim;
        PlayerLocomotion playerLocomotion;

        #region Flags
        [Header("Player Flags")]
        public bool isInteracting;
        public bool isSprinting;
        #endregion

        private void Awake()
        {

        }

        void Start()
        {
            inputHandler = GetComponent<InputHandler>();
            anim = GetComponent<Animator>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
        }

        void Update()
        {
            float delta = Time.deltaTime;
            isInteracting = anim.GetBool("isInteracting");

            inputHandler.TickInput(delta);
            playerLocomotion.HandleMovement(delta);
            playerLocomotion.HandleRollAndSprint(delta);
        }

        private void FixedUpdate()
        {
            float fdelta = Time.fixedDeltaTime;
        }

        private void LateUpdate()
        {
            inputHandler.rollFlag = false;
            inputHandler.sprintFlag = false;

            isSprinting = inputHandler.b_input;
        }
    }
}