using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J 
{
    public class DamagePlayer : MonoBehaviour
    {
        public int damage = 5;

        private void OnTriggerEnter(Collider other)
        {
            PlayerManager playerManager =  other.GetComponent<PlayerManager>();
            if (playerManager != null)
            {
                //playerManager.HandleDamage(damage);
            }
        }
    }
}