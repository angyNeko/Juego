using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J
{
    public class EnableDisableControls : MonoBehaviour
    {
        [Header("Components")]
        DialogueTrigger dialogueTrigger;

        [Header("Flags")]
        [SerializeField]
        public bool triggerDialogue;
        [SerializeField]
        bool Tutorial;

        [Header("Controls to enable")]
        [SerializeField]
        bool allControls;
        [SerializeField]
        bool Camera;
        [SerializeField]
        bool Movement;
        [SerializeField]
        bool Sprint;
        [SerializeField]
        bool baseUI;

        InputHandler inputHandler;

        private void Start()
        {
            dialogueTrigger = GetComponent<DialogueTrigger>();
            inputHandler = GameObject.Find("MainChar").GetComponent<InputHandler>();
        }

        public void TriggerEvent()
        {
            if (inputHandler == null)
                return;

            if (triggerDialogue)
            {
                dialogueTrigger.TriggerDialouge();
            }

            inputHandler.isInTutorial = Tutorial;

            if (allControls)
            {
                inputHandler.playerInputActions.PlayerMovement.Enable();
                inputHandler.playerInputActions.PlayerActions.Enable();
                inputHandler.playerInputActions.BaseUI.Enable();
            }

            if (Camera)
                inputHandler.playerInputActions.PlayerMovement.Camera.Enable();

            if (Movement)
                inputHandler.playerInputActions.PlayerMovement.Movement.Enable();

            if (Sprint)
                inputHandler.playerInputActions.PlayerActions.Roll.Enable();

            if (baseUI)
                inputHandler.playerInputActions.BaseUI.Enable();
        }
    }
}
