using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator animator;

    public void SetRunning(bool isRunning)
    {
        animator.SetBool("isRunning", isRunning);
    }

    public void SetJumping(bool isJumping)
    {
        animator.SetBool("isJumping", isJumping);
    }

    public void SetDashing(bool isDashing)
    {
        animator.SetBool("isDashing", isDashing);
    }

}
