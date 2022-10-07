using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAttackNo : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float animValue = animator.GetFloat("AttackNo");

        if (animValue == 1)
        {
            animator.SetFloat("AttackNo", 0);
        }
        else
        {
            animValue += 0.5f;
            animator.SetFloat("AttackNo", animValue);
        }
    }
}
