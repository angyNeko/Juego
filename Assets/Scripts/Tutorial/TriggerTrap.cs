using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J
{
    public class TriggerTrap : MonoBehaviour
    {
        BoxCollider childBox;

        private void OnTriggerEnter(Collider other)
        {
            GetComponentInChildren<TrapManager>().PlayAnimation();
        }

        private void OnTriggerExit(Collider other)
        {
            Destroy(this.gameObject);
        }
    }
}