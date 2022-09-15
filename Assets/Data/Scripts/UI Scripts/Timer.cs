using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace J
{
    public class Timer : MonoBehaviour
    {
        public float timeValue;

        [Header("InstantiateTrigger")]
        [SerializeField]
        GameObject triggerToBeCloned;
        [SerializeField]
        GameObject respawn;

        GameObject mainChar;

        private void Start()
        {
            mainChar = GameObject.Find("MainChar");
        }

        private void Update()
        {
            if (timeValue > 0)
            {
                timeValue -= Time.deltaTime;
                DisplayTime(timeValue);
                return;
            }

            timeValue = 0;
            Destroy(this);
        }

        private void DisplayTime(float timeToDisplay)
        {
            if (timeToDisplay < 0)
            {
                timeToDisplay = 0;
            }

            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);

            // Display time
        }
    }
}
