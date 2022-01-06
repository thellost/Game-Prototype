using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBossAttack : StateMachineBehaviour
{
    private FollowBoss followScript;
    private bool isFacingRight;
    private static int route;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        followScript = animator.GetComponent<FollowBoss>();
        isFacingRight = animator.GetBehaviour<IdleBehaviorBoss>().isFacingRight;
        route = 0;
        if (isFacingRight)
        {
            route = 1;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (followScript.coroutineAllowed)
        {
            animator.SetTrigger("idle");
        }
    }

    public int getRoute()
    {

        return route;
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
}
