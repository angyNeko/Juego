using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J {
    public class TrapManager : MonoBehaviour
    {
        Animator anim;

        private void Start()
        {
            anim = GetComponent<Animator>();
        }

        public void PlayAnimation()
        {
            anim.CrossFade("TriggerTrap", 0.1f);
        }

        private void OnTriggerEnter(Collider other)
        {
            other.GetComponent<PlayerStats>().TakeDamage(999);
            GameObject.Find("Respawn Manager").GetComponent<RespawnManager>().Invoke("TriggerRespawn", 3.5f);
        }

        private void OnTriggerExit(Collider other)
        {

            anim.CrossFade("TrapIdle", 0.1f);
        }
    }
}