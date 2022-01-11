using Chronos;
using UnityEngine;
public class DashBoss : StateMachineBehaviour
{
    [SerializeField] float dashSpeed =10;

    [SerializeField] float dashSpeedDecor = 20;
    private Vector2 target;
    private Vector2 decorationTarget;
    private Transform transform;
    private Transform decorationTransform;
    private Timeline time;
    private Vector3 prevDecorationPosition;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        decorationTransform = GameObject.FindGameObjectsWithTag("Fire")[0].transform;
        prevDecorationPosition = decorationTransform.position;
        if (animator.GetBehaviour<IdleBehaviorBoss>().isFacingRight)
        {

            target = GameObject.FindGameObjectsWithTag("Waypoint")[2].transform.position;
            decorationTarget = GameObject.FindGameObjectsWithTag("Waypoint")[4].transform.position;



            float x = Mathf.Abs(decorationTransform.localScale.x);
            decorationTransform.localScale = new Vector3(x, decorationTransform.localScale.y, decorationTransform.localScale.z);
        }
        else
        {

            target = GameObject.FindGameObjectsWithTag("Waypoint")[3].transform.position;
            decorationTarget = GameObject.FindGameObjectsWithTag("Waypoint")[5].transform.position;


            float x = -1 * Mathf.Abs(decorationTransform.localScale.x);
            decorationTransform.localScale = new Vector3(x, decorationTransform.localScale.y, decorationTransform.localScale.z);
        }
        
        transform = animator.GetComponent<Transform>();
        time = animator.GetComponent<Timeline>();
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        transform.position = Vector2.MoveTowards(transform.position, target, dashSpeed * time.deltaTime);
        if (Mathf.Abs(Vector3.Distance(target, transform.position)) < 0.001f)
        {

            decorationTransform.position = Vector2.MoveTowards(decorationTransform.position, decorationTarget, dashSpeedDecor * time.deltaTime);
            if (Mathf.Abs(Vector3.Distance(decorationTarget, decorationTransform.position)) < 0.001f)
            {

                animator.SetBool("resetAttack1", true);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        decorationTransform.position = prevDecorationPosition;
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
}
