using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;
public class idleBehaviorBoss : StateMachineBehaviour
{
    [SerializeField] float delay = 0.2f;
    public bool isFacingRight = false;
    private Transform target;
    private Transform transform;
    private Timeline time;
    private float internalTimer;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        transform = animator.GetComponent<Transform>();
        time = animator.GetComponent<Timeline>();
        internalTimer = 0;
        checkFlip();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        internalTimer += time.deltaTime;
        if(internalTimer > delay)
        {
            float value = Random.value;
            if(value <= 1f)
            {

                animator.SetTrigger("dashAttack");
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}

    private void checkFlip()
    {
        //ini mengechek flip
        if ((target.position.x - transform.position.x) < 0 && isFacingRight)
        {
            flip();
        }
        else if ((target.position.x - transform.position.x) > 0 && !isFacingRight)
        {
            flip();
        }
    }
    private void flip()
    {
            isFacingRight = !isFacingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
    }
}
