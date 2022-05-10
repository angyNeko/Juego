using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J {
    public class StartTutorial : MonoBehaviour
    {
        [SerializeField]
        GameObject startTutorial;

        private void OnTriggerEnter(Collider other)
        {
            EnableDisableControls enableDisableControls = GetComponent<EnableDisableControls>();
            enableDisableControls.triggerDialogue = true;
            enableDisableControls.TriggerEvent();
            Destroy(startTutorial);
        }
    }
}