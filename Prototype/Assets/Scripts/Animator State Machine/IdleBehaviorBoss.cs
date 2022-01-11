using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;
public class IdleBehaviorBoss : StateMachineBehaviour
{
    [SerializeField] float delay = 2f;
    public bool isFacingRight = false;
    private Transform target;
    private Transform transform;
    private Timeline time;
    private float internalTimer;
    private bool decided;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        transform = animator.GetComponent<Transform>();
        time = animator.GetComponent<Timeline>();
        internalTimer = 0;
        decided = false;
        checkFlip();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        internalTimer += time.deltaTime;
        if(internalTimer > delay)
        {
            if (!decided)
            {
                decided = true;
                float value = Random.value;
                if(value <= 0.33f)
                {
                    animator.SetTrigger("shootAttack");
                }
                else if (value <= 0.66f)
                {
                    animator.SetTrigger("dashAttack");
                }
                else
                {
                    animator.SetTrigger("jump");
                }
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        internalTimer = 0;
    }

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

    public void checkFlip()
    {
        //ini mengechek flip
        if ((target.position.x - transform.position.x) < 0 && isFacingRight)
        {

            Vector3 theScale = transform.localScale;
            theScale.x = Mathf.Abs(theScale.x) * -1;

            isFacingRight = false;
            transform.localScale = theScale;
        }
        else if ((target.position.x - transform.position.x) > 0 && !isFacingRight)
        {

            Vector3 theScale = transform.localScale;
            theScale.x = Mathf.Abs(theScale.x);

            isFacingRight = true;
            transform.localScale = theScale;
        }
    }
}
