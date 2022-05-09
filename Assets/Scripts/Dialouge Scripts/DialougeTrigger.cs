using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J
{
    public class DialougeTrigger : MonoBehaviour
    {
        public Dialouge tutorialDialouges;

        bool startedFirstDialouge;

        private void OnTriggerEnter(Collider other)
        {
            if (startedFirstDialouge)
                return;

            startedFirstDialouge = true;
            TriggerDialouge();
        }

        public void TriggerDialouge()
        {
            FindObjectOfType<DialougeManager>().StartDialouge(tutorialDialouges);
        }
    }
}
