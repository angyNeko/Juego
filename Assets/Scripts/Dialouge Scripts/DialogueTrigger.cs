using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J
{
    public class DialogueTrigger : MonoBehaviour
    {
        public Dialogue dialogues;

        public void TriggerDialouge()
        {
            FindObjectOfType<DialogueManager>().StartDialouge(dialogues);
        }
    }
}
