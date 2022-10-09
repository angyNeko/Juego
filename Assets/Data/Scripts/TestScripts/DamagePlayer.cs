using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J 
{
    public class DamagePlayer : MonoBehaviour
    {
        [SerializeField]
        int damage = 5;

        private void OnTriggerEnter(Collider other)
        {
            PlayerStats playerStats =  other.GetComponentInParent<PlayerStats>();

            if (playerStats != null)
            {
                playerStats.TakeDamage(damage);
            }
        }
    }
}