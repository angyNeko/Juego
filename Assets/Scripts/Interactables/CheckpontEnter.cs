using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J
{
    public class CheckpontEnter : MonoBehaviour
    {
        [Header("Timer Related")]
        [SerializeField]
        bool isTimed;
        [SerializeField]
        float timerValue;
        [SerializeField]
        bool isEnd;

        [Header("Flags")]
        [SerializeField]
        bool triggerDialogue;
        [SerializeField]
        public bool visible;

        [Header("Game Object References")]
        [SerializeField]
        GameObject chekcpoint;
        [SerializeField]
        MeshRenderer meshRenderer;
        [SerializeField]
        GameObject nextCheckpoint;
        [SerializeField]
        DialogueManager dialogueManager;
        [SerializeField]
        TimerManager timerManager;

        EnableDisableControls enableDisable;

        private void Start()
        {
            enableDisable = GetComponent<EnableDisableControls>();
        }

        private void Update()
        {
            if (timerManager.challengeInProgress)
                return;

            if (timerManager.timeValue <= 0 && !timerManager.challengeCompleted)
                TrialRestart();
        }

        private void LateUpdate()
        {
            GetComponent<Renderer>().enabled = visible;
            meshRenderer.enabled = visible;
            GetComponent<CapsuleCollider>().enabled = visible;
            GetComponentInChildren<Light>().enabled = visible;
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Entered object");
            if (!isTimed)
                Destroy(chekcpoint);

            if (nextCheckpoint != null)
                nextCheckpoint.GetComponent<CheckpontEnter>().visible = true;

            if (triggerDialogue)
            {
                GetComponent<DialogueTrigger>().TriggerDialouge();
            }

            enableDisable.triggerDialogue = true;
            enableDisable.TriggerEvent();

            dialogueManager.timerTrigger = isTimed;

            if (isTimed)
            {
                timerManager.SetTimer(timerValue);
                GetComponentInParent<TimedChallengeManager>().CloneFirstCheckpoint();
            }

            if (isEnd)
                TrialEnd();
        }

        private void TrialEnd()
        {
            timerManager.challengeCompleted = true;
            timerManager.DestroyTimer();
            GameObject.Find("Message Manager").GetComponent<MessageManager>().DisplaySucessMessage();

            if (GetComponentInParent<TimedChallengeManager>() != null)
                GetComponentInParent<TimedChallengeManager>().TimeTrialCompleted();
        }

        private void TrialRestart()
        {
            if (GetComponentInParent<TimedChallengeManager>() == null)
                return;

            GetComponentInParent<TimedChallengeManager>().SetStartChallengeVisible();
        }
    }
}
