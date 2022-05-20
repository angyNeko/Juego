using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J
{
    public class CreateTimer : MonoBehaviour
    {
        [Header("Ref Objects")]
        [SerializeField]
        GameObject timer;
        [SerializeField]
        GameObject timerContainer;

        [HideInInspector]
        public float timerInSeconds;

        GameObject timerClone;

        public void StartTimer()
        {
            timerClone = Instantiate(timer, this.transform) as GameObject;
            timerClone.GetComponent<Timer>().timeValue = timerInSeconds;
        }
    }
}
