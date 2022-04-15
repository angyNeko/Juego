using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlyrAnmMn : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void UpdateAnimatorValues(float HorizontalMovement, float VerticalMovement)
    {

    }
}
