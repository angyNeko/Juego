using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J
{
    public class HealPlayer : MonoBehaviour
    {
        public int heal = 5;

        private void OnTriggerEnter(Collider other)
        {
            PlayerStats playerStats =  other.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.Heal(heal);
            }
        }
    }
}