using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBasicAttack : StateMachineBehaviour
{

    [SerializeField] int minAttack = 1;

    [SerializeField] int maxAttack = 3;

    [SerializeField] float minDistanceFromPlayer = 2f;

    Transform transform;
    Vector3 waypoint1;
    Vector3 waypoint2;

    private Transform target;
    private bool isFacingRight;

    private static int attackNumber;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {


        target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        waypoint1 = GameObject.FindGameObjectsWithTag("Waypoint")[0].transform.position;
        transform = animator.GetComponent<Transform>();
        waypoint2 = GameObject.FindGameObjectsWithTag("Waypoint")[1].transform.position;

        //set random position between two waypoint
        float randomXposition = Random.Range(waypoint1.x, waypoint2.x);
        Vector2 targetPos = target.position;
        while (true)
        {
            if(Vector2.Distance(targetPos , new Vector2(randomXposition, transform.position.y)) <= minDistanceFromPlayer)
            {

                randomXposition = Random.Range(waypoint1.x, waypoint2.x);
            }
            else
            {

                break;
            }
        }
        if (attackNumber > 0)
        {
            attackNumber -= 1;
            animator.SetBool("inBasicAttack", true);
            if(attackNumber <= 0)
            {
                animator.SetBool("inBasicAttack", false);
                if (Random.value <= 0.5f)
                {
                    randomXposition = waypoint1.x;

                }
                else
                {
                    randomXposition = waypoint2.x;
                }
            }
        }
        else if(attackNumber <= 0)
        {
            attackNumber = Random.Range(minAttack+1 , maxAttack+1);
            if(Random.value <= 0.5f)
            {
                randomXposition = waypoint1.x;
                
            }
            else
            {
                randomXposition = waypoint2.x;
            }
        }
        if (animator.GetBool("resetAttack1"))
        {
            animator.SetBool("resetAttack1", false);
            if (Random.value <= 0.5f)
            {
                randomXposition = waypoint1.x;

            }
            else
            {
                randomXposition = waypoint2.x;
            }
        }


        transform.position = new Vector3(randomXposition, transform.position.y);
        animator.GetBehaviour<IdleBehaviorBoss>().checkFlip();

    }


    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
