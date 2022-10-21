using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J
{
    public class SampleInteractInherit : Interactable
    {
        public override void Interact(PInteractManager interactManager)
        {
            base.Interact(interactManager); // Calls the interact function from the Interact script

            // Insert your code here or call a created void function from within the script
        }

        //create a normal void function here
    }
}