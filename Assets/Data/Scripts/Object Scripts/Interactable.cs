using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J
{
    public class Interactable : MonoBehaviour
    {
        public float radius = 0.6f;
        public string interactableText;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, radius);
        }

        public virtual void Interact(PInteractManager interactManager)
        {
            Debug.Log("Interacted with an object");
        }
    }
}