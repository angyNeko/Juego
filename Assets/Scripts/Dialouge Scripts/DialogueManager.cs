using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace J
{
    public class DialogueManager : MonoBehaviour
    {
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI dialogueText;
        public GameObject dialogueBoxContainer;
        public GameObject dialogueBox;

        public GameObject dBoxClone;

        public InputHandler inputHandler;
        public PlayerController playerController;

        private Queue<string> sentences;

        private void Start()
        {
            sentences = new Queue<string>();
        }

        public void StartDialouge(Dialogue dialogue)
        {
            dBoxClone = Instantiate(dialogueBox, dialogueBoxContainer.transform) as GameObject;
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
                EndDialouge();
                return;
            }

            string sentence = sentences.Dequeue();
            dialogueText.text = sentence;
            Debug.Log("Next sentence: " + sentence);
        }

        public void EndDialouge()
        {
            playerController.dialogued = false;
            inputHandler.playerInputActions.UI.Disable();
            inputHandler.playerInputActions.PlayerMovement.Enable();
            inputHandler.isInUI = false;

            Destroy(dBoxClone);
            Debug.Log("End of conversation");
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