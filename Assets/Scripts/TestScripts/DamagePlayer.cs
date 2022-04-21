using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG 
{
    public class DamagePlayer : MonoBehaviour
    {
        public int damage = 5;

        private void OnTriggerEnter(Collider other)
        {
            PlayerController playerController =  other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.HandleDamage(damage);
            }
        }
    }
}