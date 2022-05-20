using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace J
{
    public class TimerManager : MonoBehaviour
    {
        public float timeValue;
        public bool timerOn;
        public bool challengeCompleted;
        public bool challengeInProgress;

        [Header("a")]
        [SerializeField]
        RespawnManager respawnManager;

        [Header("Timer")]
        [SerializeField]
        GameObject timer;
        [SerializeField]
        GameObject timerContainer;

        TextMeshProUGUI timerText;
        GameObject timerClone;

        bool timerCloned;

        private void Update()
        {
            if (!timerOn)
            {
                return;
            }

            if (timeValue <= 0)
            {
                challengeInProgress = false;
                Invoke("DestroyTimer", 1.5f);
                return;
            }

            timeValue -= Time.deltaTime;
            DisplayTime(timeValue);
            challengeInProgress = true;

        }

        public void CloneTimer()
        {
            if (timerCloned)
                return;

            timerClone = Instantiate(timer, timerContainer.transform) as GameObject;
            timerText = timerClone.GetComponent<TextMeshProUGUI>();
            timerCloned = true;
            timerOn = true;
        }

        public void SetTimer(float timervalue)
        {
            timeValue = timervalue + 1f;
        }

        private void DisplayTime(float timeToDisplay)
        {
            if (timeToDisplay < 0)
            {
                timeToDisplay = 0;
            }

            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);

            timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
        }

        public void DestroyTimer()
        {
            timerOn = false;
            timerCloned = false;
            Destroy(timerClone);
            timeValue = 0;
            CancelInvoke();

            if (!challengeCompleted)
                respawnManager.TeleportToStatue();

            challengeCompleted = false;
        }
    }
}