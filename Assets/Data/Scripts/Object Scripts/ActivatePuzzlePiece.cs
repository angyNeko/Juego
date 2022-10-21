using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J
{
    public class ActivatePuzzlePiece : Interactable
    {
        public Activatable objectToBeActivated;

        public override void Interact(PInteractManager interactManager)
        {
            base.Interact(interactManager);
            ActivateObject();
        }

        private void ActivateObject()
        {
            if (objectToBeActivated != null)
            {
                objectToBeActivated.DoSomething();
            }
            else
            {
                Debug.Log("Cant reach script");
            }
        }
    }
}