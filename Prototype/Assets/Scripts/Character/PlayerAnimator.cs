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

    public void SetRolling(bool isRolling)
    {
        animator.SetBool("isRolling", isRolling);
    }

    public void SetSpeedX(float speedX)
    {
        animator.SetFloat("Speed", speedX);
    }

    public void SetSpeedY(float speedY)
    {
        animator.SetFloat("SpeedY", speedY);
    }

    public void SetKnockback()
    {
        animator.SetTrigger("knockback");
    }
    public void SetDead(bool dead)
    {

        animator.SetBool("dead", dead);
    }

}
