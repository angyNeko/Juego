using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J
{
    public class CheckpontEnter : MonoBehaviour
    {
        [Header("Flags")]
        [SerializeField]
        bool stopPlayer;

        [SerializeField]
        GameObject chekcpoint;

        private void OnTriggerEnter(Collider other)
        {
            Destroy(chekcpoint);
            EnableDisableControls enableDisable = GetComponent<EnableDisableControls>();
            if (enableDisable == null)
                GetComponent<DialogueTrigger>().TriggerDialouge();
                return;

            enableDisable.triggerDialogue = true;
            enableDisable.TriggerEvent();
        }
    }
}
