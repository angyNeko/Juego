using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace J
{
    public class PlayerManager : CharacterManager
    {
        InputHandler inputHandler;
        PlayerLocomotion playerLocomotion;
        Animator anim;
        public CameraHandler cameraHandler;
        public GameObject cameraOverride;

        [SerializeField]
        InteractableUI interactableUI;
        public GameObject interactableUIGameObject;
        public GameObject itemInteractableGameObject;

        [Header("Player Flags")]
        public bool isInteracting;
        public bool isSprinting;
        public bool isInAir;
        public bool isGrounded;
        public bool canDoCombo;
        public bool isKnockedBack;

        /*public List<Collider> Ragdallpart = new List<Collider>();*/
        private void Awake()
        {
            InitializeCamera();
            /*SetRagdall();*/
        }

        void Start()
        {
            inputHandler = GetComponent<InputHandler>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
            anim = GetComponentInChildren<Animator>();
            interactableUI = FindObjectOfType<InteractableUI>();
        }

        //Here
        void Update()
        {
            float delta = Time.deltaTime;
            isInteracting = anim.GetBool("isInteracting");
            canDoCombo = anim.GetBool("canDoCombo");

            isSprinting = inputHandler.sprint_Input;
            inputHandler.TickInput(delta);
            playerLocomotion.HandleMovement(delta);
            playerLocomotion.HandleRollAndSprint(delta);


            CheckForInteractable();

        }

        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;

            playerLocomotion.HandleFalling(delta, playerLocomotion.moveDirection);

            if (cameraHandler != null)
            {
                cameraHandler.FollowTarget(delta, inputHandler.vertical);
                cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
            }
            else
                InitializeCamera();
        }

        // Reset flags here
        private void LateUpdate()
        {
            if (isInAir)
            {
                playerLocomotion.inAirTimer = playerLocomotion.inAirTimer + Time.deltaTime;
            }

            inputHandler.ResetBools();

            CheckForInteractable();
        }

        public void InitializeCamera()
        {
            cameraHandler = FindObjectOfType<CameraHandler>();
        }

        public void CheckForInteractable()
        {

            // Fix this in the future
            RaycastHit hit;

            if (Physics.SphereCast(transform.position, 0.3f, transform.forward, out hit, 1f, cameraHandler.ignoreLayers))
            {
                if (hit.collider.tag == "Interactable")
                {
                    Interactable interactableObject = hit.collider.GetComponent<Interactable>();

                    if(interactableObject != null)
                    {
                        string interactableText = interactableObject.interactableText;
                        interactableUI.interactableText.text = interactableText;
                        interactableUIGameObject.SetActive(true);

                        if (inputHandler.interact_Input)
                        {
                            hit.collider.GetComponent<Interactable>().Interact(this);
                        }
                    }
                }
            }
            else
            {
                if (interactableUIGameObject != null)
                {
                    interactableUIGameObject.SetActive(false);
                }

                if (itemInteractableGameObject != null && inputHandler.interact_Input)
                {
                    itemInteractableGameObject.SetActive(false);
                }
            }
        }

        public void DisplayInteractableObject()
        {

        }

        /*private void SetRagdall()
        {
            Collider[] colliders = this.GetComponentsInChildren<Collider>();

            foreach (Collider c in colliders)
            {
                if (c.gameObject != this.gameObject)
                {
                    c.isTrigger = true;
                    Ragdallpart.Add(c);
                }
            }
        }*/
        private void isTriggerRagdall()
        {

        }
    }
}