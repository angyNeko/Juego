using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace J
{
    public class AnimatorManager : MonoBehaviour
    {
        PlayerController playerController;
        public Animator anim;

        int horizontal;
        int vertical;
        public bool canRotate;
        bool isInteracting;

        public void Initialize()
        {
            playerController = GetComponent<PlayerController>();
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
            float snappedHorizontal = 0;

            if (horizontalMovement > 0 && horizontalMovement < 0.55f)
            {
                snappedHorizontal = 0.5f;
            }
            else if (horizontalMovement > 0.55f)
            {
                snappedHorizontal = 1;
            }
            else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
            {
                snappedHorizontal = -0.5f;
            }
            else if (horizontalMovement < -0.55f)
            {
                snappedHorizontal = -1;
            }
            else
            {
                snappedHorizontal = 0;
            }
            #endregion

            #region Snapped Vertical
            float snappedVertical;

            if (verticalMovement > 0 && verticalMovement < 0.55f)
            {
                snappedVertical = 0.5f;
            }
            else if (verticalMovement > 0.55f)
            {
                snappedVertical = 1;
            }
            else if (verticalMovement < 0 && verticalMovement > -0.55f)
            {
                snappedVertical = -0.5f;
            }
            else if (verticalMovement < -0.55f)
            {
                snappedVertical = -1;
            }
            else
            {
                snappedVertical = 0;
            }
            #endregion

            if (isSprinting)
            {
                snappedVertical = 2;
                snappedHorizontal = horizontalMovement;
            }

            if (playerController.dialogued)
            {
                snappedHorizontal = 0f;
                snappedVertical = 0f;
            }

            anim.SetFloat(horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
            anim.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);
        }

        public void CanRotate()
        {
            canRotate = true;
        }

        public void StopRotation()
        {
            canRotate = true;
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