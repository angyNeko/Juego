using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace J
{
    public class DialougeManager : MonoBehaviour
    {
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI dialougeText;
        public GameObject dialougeBox;
        public GameObject dialougeBoxContainer;

        public InputHandler inputHandler;

        private Queue<string> sentences;

        private void Start()
        {
            sentences = new Queue<string>();
        }

        public void StartDialouge(Dialouge dialouge)
        {
            if (dialougeBox == null)
            {
                Instantiate(dialougeBox, dialougeBoxContainer.transform);
            }
            inputHandler.playerInputActions.UI.Enable();
            inputHandler.isInUI = true;

            nameText.text = dialouge.name;

            sentences.Clear();

            foreach (string sentence in dialouge.sentences)
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
            dialougeText.text = sentence;
            Debug.Log("Next sentence: " + sentence);
        }

        public void EndDialouge()
        {
            inputHandler.playerInputActions.UI.Disable();
            inputHandler.isInUI = false;
            Destroy(dialougeBox);
            Debug.Log("End of conversation");
        }
    }
}