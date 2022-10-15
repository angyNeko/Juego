using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandAnimalLocomotion : MonoBehaviour
{
    CharacterController animalController;
    

    void Start()
    {
        animalController = GetComponent<CharacterController>();
    }
}
