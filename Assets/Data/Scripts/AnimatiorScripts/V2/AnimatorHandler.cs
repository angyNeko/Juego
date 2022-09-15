using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J
{
    public class AnimatorHandler : MonoBehaviour
    {
        public Animator anim;

        int horizontal;
        int vertical;
        public bool canRotate;
        bool isInteracting;

        public void Initialize()
        {
            anim = GetComponent<Animator>();
            horizontal = Animator.StringToHash("Horizontal");
            vertical = Animator.StringToHash("Vertical");
        }

        private void Update()
        {
            //OnAnimatorMove();
        }

        public void PlayTargetAnimation(string targetAnimation, bool isInteracting)
        {
            anim.applyRootMotion = isInteracting;
            anim.SetBool("isInteracting", isInteracting);
            //OnAnimatorMove();
            anim.CrossFade(targetAnimation, 0.01f);
        }

        public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement, bool isSprinting)
        {
            #region Snapped horizontal
            float h = 0;

            if (horizontalMovement > 0 && horizontalMovement < 0.55f)
            {
                h = 0.5f;
            }
            else if (horizontalMovement > 0.55f)
            {
                h = 1;
            }
            else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
            {
                h = -0.5f;
            }
            else if (horizontalMovement < -0.55f)
            {
                h = -1;
            }
            else
            {
                h = 0;
            }
            #endregion

            #region Snapped Vertical
            float v;

            if (verticalMovement > 0 && verticalMovement < 0.55f)
            {
                v = 0.5f;
            }
            else if (verticalMovement > 0.55f)
            {
                v = 1;
            }
            else if (verticalMovement < 0 && verticalMovement > -0.55f)
            {
                v = -0.5f;
            }
            else if (verticalMovement < -0.55f)
            {
                v = -1;
            }
            else
            {
                v = 0;
            }
            #endregion

            if (isSprinting)
            {
                v = 2;
                h = horizontalMovement;
            }

            anim.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
            anim.SetFloat(vertical, v, 0.1f, Time.deltaTime);
        }

        public void CanRotate()
        {
            canRotate = true;
        }

        public void StopRotation()
        {
            canRotate = false;
        }
        /*
        private void OnAnimatorMove()
        {
            if (playerController.inputHandler.isInteracting == false)
                return;

            float delta = Time.deltaTime;
            playerController.playerLocomotion.characterModel.drag = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            playerController.playerLocomotion.characterModel.velocity = velocity;
        }
        */
    }
}