using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J
{
    public class PlayerManager : MonoBehaviour
    {
        InputHandler inputHandler;
        Animator anim;

        [Header("Flags")]
        public bool isInteracting;
        public bool applyRootMotion;
        
        void Start()
        {
            inputHandler = GetComponent<InputHandler>();
            anim = GetComponentInChildren<Animator>();
        }

        void FixedUpdate()
        {
            isInteracting = anim.GetBool("isInteracting");
            applyRootMotion = anim.applyRootMotion;
            inputHandler.rollFlag = false;
        }
    }
}