using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace J
{
    public class InteractManager : MonoBehaviour
    {
        [Header("Flags")]
        [SerializeField]
        public bool statue;
        [SerializeField]
        public bool item;
        [SerializeField]
        public bool doors;

        [Header("References")]
        [SerializeField]
        GameObject buttonKey;                   // reference to gamepad buddon or keyboard key
        [SerializeField]
        GameObject buttonContainer;
        [SerializeField]
        GameObject interactButton;
        [SerializeField]
        GameObject interactContainer;

        [Header("Script Reference")]
        [SerializeField]
        MessageManager messageManager;

        [Header("Controlled by Script")]
        public GameObject respawnPoint;
        public string itemName;
        public int itemAmount;

        bool buttonDisplayed;
        GameObject interactButtonClone;
        GameObject buttonKeyClone;

        public void CloneInteractButton(string objectntName)
        {
            if (!buttonDisplayed)
            {
                buttonKeyClone = Instantiate(buttonKey, buttonContainer.transform) as GameObject;

                interactButtonClone = Instantiate(interactButton, interactContainer.transform) as GameObject;
                interactButtonClone.GetComponentInChildren<TextMeshProUGUI>().text = objectntName;

                buttonDisplayed = true;
            }
        }

        public void DestroyInteractButton(string objectName)
        {
            if (!buttonDisplayed)
                return;

            if (interactButtonClone == null && buttonKeyClone == null)
                return;

            Destroy(interactButtonClone);
            Destroy(buttonKeyClone);
            buttonDisplayed = false;
        }

        public void InteractsWithObject()
        {
            if (statue)
            {
                //respawnManager.SetRespawn(respawnPoint);
                //messageManager.DisplayMessage();
            }

            if (item)
                return; //triggers obtainableManager

            if (doors)
                return; //triggers doorManager

        }
    }
}