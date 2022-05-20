using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace J
{
    public class MessageManager : MonoBehaviour
    {
        [SerializeField]
        GameObject messageText;
        [SerializeField]
        GameObject messageContainer;

        bool messageShown;
        GameObject messageClone;


        public void DisplayMessage(string messag)
        {
            if (messageShown)
                return;

            messageClone = Instantiate(messageText, messageContainer.transform);
            messageClone.GetComponent<TextMeshProUGUI>().text = messag;

            Destroy(messageClone, 2f);
        }

        public void DisplaySucessMessage()
        {
            messageClone = Instantiate(messageText, messageContainer.transform);
            messageClone.GetComponent<TextMeshProUGUI>().text = "Success";
            Destroy(messageClone, 2f);
        }
    }
}