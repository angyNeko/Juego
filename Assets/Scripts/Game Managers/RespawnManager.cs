using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J
{
    public class RespawnManager : MonoBehaviour
    {
        GameObject prevRespawnObject;
        public GameObject respawnObject;
        [SerializeField]
        MessageManager messageManager;

        public GameObject challenge;

        [SerializeField]
        GameObject mainChar;

        GameObject player;

        public void TeleportToStatue()
        {
            if (respawnObject == null)
                return;

            mainChar.GetComponent<InputHandler>().playerInputActions.PlayerMovement.Movement.Disable();
            mainChar.transform.position = respawnObject.transform.position;
            mainChar.transform.rotation = respawnObject.transform.rotation;
            mainChar.GetComponent<InputHandler>().playerInputActions.PlayerMovement.Movement.Enable();
        }

        public void TriggerRespawn()
        {
            if (respawnObject == null)
                return;

            mainChar.GetComponent<InputHandler>().playerInputActions.PlayerMovement.Movement.Disable();
            mainChar.transform.position = respawnObject.transform.position;
            mainChar.transform.rotation = respawnObject.transform.rotation;

            mainChar.GetComponent<PlayerStats>().Invoke("Revive", 2f);

            mainChar.GetComponent<InputHandler>().playerInputActions.PlayerMovement.Movement.Enable();
        }

        public void SetRespawn(GameObject respawn)
        {
            if (prevRespawnObject != null)
            {
                prevRespawnObject = respawnObject;
            }

            respawnObject = respawn;

            messageManager.DisplayMessage("Respawn Set");
        }

        private void OnTriggerExit(Collider other)
        {
            CancelInvoke("Heal");
        }

        void Heal()
        {
            player.GetComponent<PlayerStats>().Heal(999);
        }
    }
}