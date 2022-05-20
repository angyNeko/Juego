using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace J
{
    public class DialogueManager : MonoBehaviour
    {
        [Header("Managed by script")]
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI dialogueText;
        public GameObject dBoxClone;

        [Header("Reference Objects")]
        public GameObject dialogueBoxContainer;
        public GameObject dialogueBox;
        public InputHandler inputHandler;
        public PlayerController playerController;
        [SerializeField]
        TimerManager timerManager;

        [Header("Flags")]
        public bool timerTrigger;

        private Queue<string> sentences;


        bool dboxAvailable;

        private void Start()
        {
            sentences = new Queue<string>();
        }

        public void StartDialouge(Dialogue dialogue)
        {
            if (!dboxAvailable)
            {
                dBoxClone = Instantiate(dialogueBox, dialogueBoxContainer.transform) as GameObject;
                dboxAvailable = true;
            }

            nameText = GameObject.Find("Name").GetComponent<TextMeshProUGUI>();
            dialogueText = GameObject.Find("Dialouge").GetComponent<TextMeshProUGUI>();
            StopPlayerFirst();

            nameText.text = dialogue.name;

            sentences.Clear();

            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }

            DisplayNextSentence();
        }

        public void DisplayNextSentence()
        {
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }

            string sentence = sentences.Dequeue();
            dialogueText.text = sentence;
            Debug.Log("Next sentence: " + sentence);
        }

        public void EndDialogue()
        {
            ReEnableControls();
            Destroy(dBoxClone);
            dboxAvailable = false;

            if (timerTrigger)
                timerManager.Invoke("CloneTimer", 0.75f);
        }

        public void ReEnableControls()
        {
            playerController.dialogued = false;
            inputHandler.playerInputActions.UI.Disable();
            inputHandler.playerInputActions.PlayerMovement.Enable();
            inputHandler.isInUI = false;
        }

        private void StopPlayerFirst()
        {
            playerController.dialogued = true;

            inputHandler.playerInputActions.PlayerMovement.Disable();
            inputHandler.playerInputActions.UI.Enable();
            inputHandler.isInUI = true;
        }
    }
}